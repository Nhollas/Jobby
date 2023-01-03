using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
internal sealed class MoveJobCommandHandler : IRequestHandler<MoveJobCommand, Unit>
{
    private readonly IRepository<Job> _jobRepository;
    private readonly IRepository<JobList> _jobListRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public MoveJobCommandHandler(
        IRepository<Job> jobRepository,
        IRepository<JobList> jobListRepository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _jobRepository = jobRepository;
        _jobListRepository = jobListRepository;
        _userService = userService;
        _userId = _userService.UserId();
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(MoveJobCommand request, CancellationToken cancellationToken)
    {
        Job jobToMove = await _jobRepository.GetByIdAsync(request.JobId, cancellationToken);

        if (jobToMove == null)
        {
            throw new NotFoundException($"A job with id {request.JobId} could not be found.");
        }

        if (jobToMove.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        jobToMove.SetJobList(request.TargetJobListId);
        jobToMove.UpdateEntity(_timeProvider.UtcNow);

        await _jobRepository.UpdateAsync(jobToMove, cancellationToken);

        JobList targetJobList = await _jobListRepository.GetByIdAsync(request.TargetJobListId, cancellationToken);

        if (targetJobList.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        targetJobList.ChangeJobPosition(request.JobId, request.TargetIndex);
        targetJobList.UpdateEntity(_timeProvider.UtcNow);
        await _jobListRepository.UpdateAsync(targetJobList, cancellationToken);

        return Unit.Value;
    }
}
