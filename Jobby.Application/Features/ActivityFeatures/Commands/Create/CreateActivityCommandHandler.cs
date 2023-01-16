using FluentValidation;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

internal sealed class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IRepository<Activity> _activityRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateActivityCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider,
        IRepository<Activity> activityRepository)
    {
        _boardRepository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
        _activityRepository = activityRepository;
    }

    /*
        An Activity is created by it being added to a board entity (parent).
        The Activity can be added to a specific job on the board as well if it's not null.
    */
    public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var boardSpec = new GetBoardByIdSpec(request.BoardId);

        Board BoardToLink = await _boardRepository.FirstOrDefaultAsync(boardSpec, cancellationToken);

        if (BoardToLink is null)
        {
            throw new NotFoundException($"A board with id {request.BoardId} could not be found.");
        }

        if (BoardToLink.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        var createdActivity = Activity.Create(
            _guidProvider.Id,
            _dateTimeProvider.UtcNow,
            _userId,
            request.Title,
            request.ActivityType,
            request.StartDate,
            request.EndDate,
            request.Note,
            request.Completed,
            BoardToLink);

        if (request.JobId != Guid.Empty)
        {
            if (!BoardOwnsJob(BoardToLink, request.JobId))
            {
                throw new NotFoundException($"The {nameof(Job)} {request.JobId} you wanted to link doesn't exist in the Board {request.BoardId}.");
            }

            Job jobToLink = BoardToLink.JobList
                                .SelectMany(x => x.Jobs)
                                .Where(x => x.Id == request.JobId)
                                .First();

            createdActivity.SetJob(jobToLink);  
        }

        await _activityRepository.AddAsync(createdActivity, cancellationToken);

        return createdActivity.Id;
    }

    private static bool BoardOwnsJob(Board board, Guid jobId)
    {
        return board.JobList
            .SelectMany(x => x.Jobs
            .Where(x => x.Id == jobId))
            .Any();
    }
}
