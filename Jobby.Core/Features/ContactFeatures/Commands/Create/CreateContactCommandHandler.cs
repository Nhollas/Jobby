using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using Jobby.Core.Specifications;
using MediatR;

namespace Jobby.Core.Features.ContactFeatures.Commands.Create;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
{
    private readonly IRepository<Job> _jobRepository;
    private readonly IRepository<Board> _boardRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateContactCommandHandler(
    IUserService userService,
    IRepository<Board> boardRepository,
    IRepository<Job> jobRepository)
    {
        _userService = userService;
        _userId = _userService.UserId();
        _boardRepository = boardRepository;
        _jobRepository = jobRepository;
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
            // TODO: NotFound Problem Details.
        }

        if (boardTolink.OwnerId != _userId)
        {
            // TODO: NotAuthorised Problem Details.
        }

        Contact createdContact = new(
            request.FirstName,
            request.LastName,
            request.JobTitle,
            new Social(request.TwitterUri, request.FacebookUri, request.LinkedInUri, request.GithubUri),
            request.BoardId,
            _userId,
            request.Companies,
            request.Emails,
            request.Phones);

        await LinkContactToBoard(createdContact, boardTolink);

        if (request.JobIds != null)
        {
            var ownedJobs = boardTolink.JobList.Select(x => x.Id);

            if (!JobIdsInBoard(ownedJobs, request.JobIds))
            {
                // TODO: NotAuthorised Problem Details.
            }

            await LinkContactToJobs(createdContact, request.JobIds);
        }

        return createdContact.Id;
    }

    private async Task LinkContactToBoard(Contact contact, Board board)
    {
        board.AddContact(contact);

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

    private static bool JobIdsInBoard(IEnumerable<Guid> ownedJobs, Guid[] jobIds)
    {
        for (int i = 0; i < jobIds.Length; i++)
        {
            if (ownedJobs.Contains(jobIds[i]))
                return true;
        }
        return false;
    }
}
