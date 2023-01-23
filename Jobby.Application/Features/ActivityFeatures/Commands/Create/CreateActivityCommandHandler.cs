using FluentValidation;
using Jobby.Application.Abstractions.Authorization;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

internal sealed class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
{
    private readonly IResource<Board> _resourceChecker;
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
        IRepository<Activity> activityRepository,
        IResource<Board> resourceChecker)
    {
        _boardRepository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
        _activityRepository = activityRepository;
        _resourceChecker = resourceChecker;
    }

    public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var boardToLink = await _resourceChecker
            .GetBy(_boardRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetBoardByIdSpec(request.BoardId))
            .Check(_userId, request.BoardId);

        var createdActivity = Activity.Create(
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Title,
            request.Type,
            request.StartDate,
            request.EndDate,
            request.Note,
            request.Completed,
            boardToLink);

        if (request.JobId != Guid.Empty)
        {
            if (!BoardOwnsJob(boardToLink, request.JobId))
            {
                throw new NotFoundException($"The {nameof(Job)} {request.JobId} you wanted to link doesn't exist in the Board {request.BoardId}.");
            }

            Job jobToLink = boardToLink.JobList
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
