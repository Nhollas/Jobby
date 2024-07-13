using AutoMapper;
using Jobby.Application.Dtos;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetById;
internal class GetJobDetailQueryHandler(
    IUserService userService,
    IMapper mapper,
    IReadRepository<Job> jobRepository)
    : IRequestHandler<GetJobDetailQuery, IDispatchResult<JobDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<JobDto>> Handle(GetJobDetailQuery request, CancellationToken cancellationToken)
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
        
        return DispatchResults.Ok(mapper.Map<JobDto>(job));
    }
}
