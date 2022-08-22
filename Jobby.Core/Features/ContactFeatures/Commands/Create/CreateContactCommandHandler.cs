using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

internal sealed class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
{
    private readonly IRepository<Job> _jobRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IRepository<Contact> _contactRepository;
    private readonly IRepository<Board> _boardRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateContactCommandHandler(
    IUserService userService,
    IRepository<Board> boardRepository,
    IRepository<Job> jobRepository,
    IDateTimeProvider dateTimeProvider,
    IRepository<Contact> contactRepository)
    {
        _userService = userService;
        _userId = _userService.UserId();
        _boardRepository = boardRepository;
        _jobRepository = jobRepository;
        _dateTimeProvider = dateTimeProvider;
        _contactRepository = contactRepository;
    }

    /*
        A Contact is created by it being added to a board entity (parent).
        The Contact can be added to multiple jobs that the board owns as well.
    */
    public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var boardSpec = new IncludeJobListSpecification(request.BoardId);

        Board boardTolink = await _boardRepository.FirstOrDefaultAsync(boardSpec, cancellationToken);

        if (boardTolink is null)
        {
            throw new NotFoundException($"A board with id {request.BoardId} could not be found.");
        }

        if (boardTolink.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        Contact contact = new(
            Guid.NewGuid(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.FirstName,
            request.LastName,
            request.JobTitle,
            new Social(request.TwitterUri, request.FacebookUri, request.LinkedInUri, request.GithubUri),
            request.BoardId,
            request.Companies,
            request.Emails,
            request.Phones);

        var newContact = await _contactRepository.AddAsync(contact, cancellationToken);

        await LinkContactToBoard(newContact, boardTolink);

        if (request.JobIds != null)
        {
            var ownedJobs = boardTolink.JobList
                .SelectMany(x => x.Jobs)
                .ToList();

            List<Guid> ownedJobIds = new();

            foreach (var job in ownedJobs)
            {
                ownedJobIds.Add(job.Id);
            }

            if (!JobIdsInBoard(ownedJobIds, request.JobIds))
            {
                throw new NotAuthorisedException(_userId);
            }

            await LinkContactToJobs(newContact, request.JobIds);
        }

        return newContact.Id;
    }

    private async Task LinkContactToBoard(Contact contact, Board board)
    {
        board.Contacts.Add(contact);

        await _boardRepository.UpdateAsync(board);
    }

    private async Task LinkContactToJobs(Contact createdContact, Guid[] selectedJobIds)
    {
        var getSelectJobsSpec = new GetSelectedJobSpecification(selectedJobIds);

        var selectedJobs = await _jobRepository.ListAsync(getSelectJobsSpec);

        foreach (var job in selectedJobs)
        {
            JobContact contact = new(createdContact, job);

            job.AddContact(contact);
        }

        await _jobRepository.UpdateRangeAsync(selectedJobs);
    }

    private static bool JobIdsInBoard(List<Guid> ownedJobIds, Guid[] jobIds)
    {
        bool result = true;

        for (int i = 0; i < jobIds.Length; i++)
        {
            if (ownedJobIds.Contains(jobIds[i]))
            {
            }
            result = false;
        }

        return result;
    }
}
