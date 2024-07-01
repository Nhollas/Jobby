using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Specifications;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListActivities;

internal class GetJobActivityListQueryHandler(
    IUserService userService,
    IMapper mapper,
    IReadRepository<Activity> activityRepository)
    : IRequestHandler<GetJobActivityListQuery, IDispatchResult<List<ActivityDto>>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<List<ActivityDto>>> Handle(GetJobActivityListQuery request, CancellationToken cancellationToken)
    {
        GetJobActivitiesSpecification jobSpec = new(request.JobReference, _userId);

        List<Activity> activities = await activityRepository.ListAsync(jobSpec, cancellationToken);

        return DispatchResults.Ok(mapper.Map<List<ActivityDto>>(activities));
    }
}