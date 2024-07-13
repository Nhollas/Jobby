using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Delete;

internal class DeleteJobCommandHandler(
    IRepository<Job> jobRepository,
    IUserService userService)
    : IRequestHandler<DeleteJobCommand, IDispatchResult<DeleteJobResponse>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<DeleteJobResponse>> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        Job? job = await jobRepository.GetByReferenceAsync(request.JobReference, cancellationToken);
        
        if (job is null)
        {
            return DispatchResults.NotFound<DeleteJobResponse>(request.JobReference);
        }
        
        if (!job.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<DeleteJobResponse>(job.Reference);
        }

        await jobRepository.DeleteAsync(job, cancellationToken);

        return DispatchResults.Ok(new DeleteJobResponse());
    }
}
