using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.LinkJob;
internal sealed class LinkJobCommandHandler : IRequestHandler<LinkJobCommand, Unit>
{
    private readonly IRepository<Activity> _activityRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public LinkJobCommandHandler(
    IRepository<Activity> activityRepository,
    IUserService userService)
    {
        _activityRepository = activityRepository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(LinkJobCommand request, CancellationToken cancellationToken)
    {
        var activitySpec = new GetActivityWithBoardJobsSpec(request.ActivityId);
        var activity = await _activityRepository.FirstOrDefaultAsync(activitySpec, cancellationToken);

        if (activity is null)
        {
            throw new NotFoundException($"An Activity with id {request.ActivityId} could not be found.");
        }

        if (activity.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        var selectedJob = activity.Board.Jobs.Where(job => job.Id == request.JobId).FirstOrDefault();

        if (selectedJob is null)
        {
            throw new NotFoundException($"The Job {request.JobId} could not be found.");
        }

        activity.SetJob(selectedJob);

        await _activityRepository.UpdateAsync(activity, cancellationToken);

        return Unit.Value;
    }
}
