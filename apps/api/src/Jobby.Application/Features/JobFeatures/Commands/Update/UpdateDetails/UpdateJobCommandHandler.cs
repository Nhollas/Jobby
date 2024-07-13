using AutoMapper;
using Jobby.Application.Dtos;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
internal class UpdateJobCommandHandler(
    IRepository<Job> jobRepository,
    IUserService userService,
    IMapper mapper,
    TimeProvider timeProvider)
    : IRequestHandler<UpdateJobCommand, IDispatchResult<JobDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<JobDto>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        Job? job = await jobRepository.GetByReferenceAsync(request.JobReference, cancellationToken);

        if (job is null)
        {
            return DispatchResults.NotFound<JobDto>(request.JobReference);
        }
        
        if (!job.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<JobDto>(job.Reference);
        }

        mapper.Map(request, job, typeof(UpdateJobCommand), typeof(Job));

        job.UpdateEntity(timeProvider.GetUtcNow());

        await jobRepository.UpdateAsync(job, cancellationToken);
        
        return DispatchResults.Ok(new JobDto());
    }
}
