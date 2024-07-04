using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
internal class MoveJobCommandHandler(
    IRepository<Job> jobRepository,
    IRepository<JobList> jobListRepository,
    IUserService userService,
    TimeProvider timeProvider)
    : IRequestHandler<MoveJobCommand, IDispatchResult<MoveJobResponse>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<MoveJobResponse>> Handle(MoveJobCommand request, CancellationToken cancellationToken)
    {
        Job? job = await jobRepository.GetByReferenceAsync(request.JobReference, cancellationToken);
        
        if (job is null)
        {
            return DispatchResults.NotFound<MoveJobResponse>(request.JobReference);
        }
        
        if (!job.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<MoveJobResponse>("You are not authorized to move this job");
        }

        // TODO: Fetch the target job list and set it on the job
        JobList? jobList = await jobListRepository.GetByReferenceAsync(request.JobListReference, cancellationToken);
        
        if (jobList is null)
        {
            return DispatchResults.NotFound<MoveJobResponse>(request.JobListReference);
        }
        
        job.SetJobList(jobList);
        job.UpdateEntity(timeProvider.GetUtcNow());

        await jobRepository.UpdateAsync(job, cancellationToken);

        return DispatchResults.Ok<MoveJobResponse>(new MoveJobResponse());
    }
}
