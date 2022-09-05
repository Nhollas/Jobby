using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Activity;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Queries.ListBoardActivities;
internal sealed class GetBoardActivityListQueryHandler : IRequestHandler<GetBoardActivityListQuery, List<ListActivitiesResponse>>
{
    private readonly IRepository<Activity> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetBoardActivityListQueryHandler(
        IRepository<Activity> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<List<ListActivitiesResponse>> Handle(GetBoardActivityListQuery request, CancellationToken cancellationToken)
    {
        var activitySpec = new ListBoardActivitiesSpec(request.BoardId, _userId);

        var activityList = await _repository.ListAsync(activitySpec, cancellationToken);

        if (activityList is null)
        {
            return new List<ListActivitiesResponse>();
        }

        return _mapper.Map<List<ListActivitiesResponse>>(activityList);
    }
}
