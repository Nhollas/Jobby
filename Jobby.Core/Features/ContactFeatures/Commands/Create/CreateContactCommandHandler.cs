using AutoMapper;
using Jobby.Core.Entities.BoardAggregate;
using Jobby.Core.Entities.ContactAggregate;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.ContactFeatures.Commands.Create;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly IReadRepository<Board> _boardRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateContactCommandHandler(
    IRepository<Contact> contactRepository,
    IUserService userService,
    IReadRepository<Board> boardRepository)
    {
        _contactRepository = contactRepository;
        _userService = userService;
        _userId = _userService.UserId();
        _boardRepository = boardRepository;
    }

    public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        Board boardTolink = await _boardRepository.GetByIdAsync(request.BoardId, cancellationToken);

        if (boardTolink == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (boardTolink.OwnerId != _userId)
        {
            // TODO: NotAuthorised Problem Details.
        }

        var ownedJobs = boardTolink.JobList.Select(x => x.Id);

        if (!JobIdsInBoard(ownedJobs, request.JobIds))
        {
            // TODO: NotAuthorised Problem Details.
        }

        Contact mockContact = new(
            request.FirstName,
            request.LastName,
            request.JobTitle,
            new Social(request.TwitterUri, request.FacebookUri, request.LinkedInUri, request.GithubUri),
            request.BoardId,
            _userId);

        var convertedJobIds = request.JobIds.Select(g => g.ToString()).ToArray();

        mockContact.Join(convertedJobIds, request.Companies, request.Emails, request.Phones);

        var createdContact = await _contactRepository.AddAsync(mockContact, cancellationToken);

        return createdContact.Id;
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
