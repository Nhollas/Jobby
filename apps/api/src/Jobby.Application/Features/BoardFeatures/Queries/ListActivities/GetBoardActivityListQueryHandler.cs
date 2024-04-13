using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListActivities;
internal sealed class GetBoardActivityListQueryHandler : IRequestHandler<GetBoardActivityListQuery, List<ActivityDto>>
{
    private readonly IReadRepository<Activity> _repository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public GetBoardActivityListQueryHandler(
        IReadRepository<Activity> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userId = userService.UserId();
        _mapper = mapper;
    }

    public async Task<List<ActivityDto>> Handle(GetBoardActivityListQuery request, CancellationToken cancellationToken)
    {
        GetActivitiesFromBoardSpecification activitySpec = new GetActivitiesFromBoardSpecification(request.BoardReference, _userId);

        List<Activity> activityList = await _repository.ListAsync(activitySpec, cancellationToken);

        return _mapper.Map<List<ActivityDto>>(activityList);
    }
}
