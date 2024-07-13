using AutoMapper;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListActivities;
internal class GetBoardActivityListQueryHandler(
    IReadRepository<Activity> activityRepository,
    IUserService userService,
    IMapper mapper)
    : IRequestHandler<GetBoardActivityListQuery, IDispatchResult<List<ActivityDto>>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<List<ActivityDto>>> Handle(GetBoardActivityListQuery request, CancellationToken cancellationToken)
    {
        List<Activity> activityList = await activityRepository.ListAsync(new GetActivitiesFromBoardSpecification(request.BoardReference, _userId), cancellationToken);

        return DispatchResults.Ok(mapper.Map<List<ActivityDto>>(activityList));
    }
}
