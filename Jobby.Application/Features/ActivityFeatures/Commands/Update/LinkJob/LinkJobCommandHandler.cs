using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update.LinkJob;
internal sealed class LinkJobCommandHandler : IRequestHandler<LinkJobCommand, Unit>
{
    private readonly IRepository<Activity> _activityRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly string _userId;

    public LinkJobCommandHandler(
    IRepository<Activity> activityRepository,
    IUserService userService,
    IRepository<Job> jobRepository)
    {
        _activityRepository = activityRepository;
        _userId = userService.UserId();
        _jobRepository = jobRepository;
    }

    public async Task<Unit> Handle(LinkJobCommand request, CancellationToken cancellationToken)
    {
        Activity activity = await ResourceProvider<Activity>
            .GetById(_activityRepository.GetByIdAsync)
            .Check(_userId, request.ActivityId, cancellationToken);

        Job selectedJob = await ResourceProvider<Job>
            .GetById(_jobRepository.GetByIdAsync)
            .Check(_userId, request.JobId, cancellationToken);

        activity.SetJob(selectedJob);

        await _activityRepository.UpdateAsync(activity, cancellationToken);

        return Unit.Value;
    }
}
