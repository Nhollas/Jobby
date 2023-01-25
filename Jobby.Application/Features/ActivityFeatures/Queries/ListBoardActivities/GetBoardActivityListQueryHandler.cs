using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Activity;
using Jobby.Application.Features.ActivityFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Queries.ListBoardActivities;
internal sealed class GetBoardActivityListQueryHandler : IRequestHandler<GetBoardActivityListQuery, List<ListActivitiesResponse>>
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

    public async Task<List<ListActivitiesResponse>> Handle(GetBoardActivityListQuery request, CancellationToken cancellationToken)
    {
        var activitySpec = new GetActivitiesFromBoardSpecification(request.BoardId, _userId);

        var activityList = await _repository.ListAsync(activitySpec, cancellationToken);

        return _mapper.Map<List<ListActivitiesResponse>>(activityList);
    }
}
