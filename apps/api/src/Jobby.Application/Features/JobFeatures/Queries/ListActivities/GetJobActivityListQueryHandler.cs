using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListActivities;

internal sealed class GetJobActivityListQueryHandler : IRequestHandler<GetJobActivityListQuery, List<ActivityDto>>
{
    private readonly IReadRepository<Activity> _activityRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;
    
    public GetJobActivityListQueryHandler(
        IUserService userService,
        IMapper mapper,
        IReadRepository<Activity> activityRepository)
    {
        _userId = userService.UserId();
        _mapper = mapper;
        _activityRepository = activityRepository;
    }

    public async Task<List<ActivityDto>> Handle(GetJobActivityListQuery request, CancellationToken cancellationToken)
    {
        GetJobActivitiesSpecification jobSpec = new GetJobActivitiesSpecification(request.JobReference, _userId);

        List<Activity> activities = await _activityRepository.ListAsync(jobSpec, cancellationToken);

        return _mapper.Map<List<ActivityDto>>(activities);
    }
}