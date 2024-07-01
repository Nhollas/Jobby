using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Specifications;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetList;

internal class GetJobListQueryHandler(
    IUserService userService,
    IMapper mapper,
    IReadRepository<Job> jobRepository)
    : IRequestHandler<GetJobListQuery, IDispatchResult<List<JobDto>>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<List<JobDto>>> Handle(GetJobListQuery request, CancellationToken cancellationToken)
    {
        List<Job> jobs = await jobRepository.ListAsync(new GetJobsFromUserSpecification(_userId), cancellationToken);

        return DispatchResults.Ok(mapper.Map<List<JobDto>>(jobs));
    }
}