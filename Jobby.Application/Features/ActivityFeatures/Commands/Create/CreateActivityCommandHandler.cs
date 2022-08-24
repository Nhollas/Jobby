using FluentValidation;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

internal sealed class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IRepository<Job> _jobRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateActivityCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IRepository<Job> jobRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _boardRepository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _jobRepository = jobRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    /*
        An Activity is created by it being added to a board entity (parent).
        The Activity can be added to a specific job on the board as well if it's not null.
    */
    public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateActivityCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

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
            _dateTimeProvider.UtcNow,
            _userId,
            request.Title,
            request.ActivityType,
            request.StartDate,
            request.EndDate,
            request.Note,
            request.Completed);


        await LinkActivityToBoard(createdActivity, BoardToLink);

        if (request.JobId != Guid.Empty)
        {
            if (!BoardOwnsJob(BoardToLink, request.JobId))
            {
                throw new NotFoundException($"The {nameof(Job)} {request.JobId} you wanted to link doesn't exist in the Board {request.BoardId}.");
            }

            await LinkActivityToJob(createdActivity, request.JobId);
        }

        return createdActivity.Id;
    }

    private async Task LinkActivityToJob(Activity activity, Guid jobId)
    {
        Job job = await _jobRepository.GetByIdAsync(jobId);

        job.AddActivity(activity);

        await _jobRepository.UpdateAsync(job);
    }

    private async Task LinkActivityToBoard(Activity activity, Board board)
    {
        board.AddActivity(activity);

        await _boardRepository.UpdateAsync(board);
    }

    private static bool BoardOwnsJob(Board board, Guid jobId)
    {
        return board.JobList
            .SelectMany(x => x.Jobs
            .Where(x => x.Id == jobId))
            .Any();
    }
}
