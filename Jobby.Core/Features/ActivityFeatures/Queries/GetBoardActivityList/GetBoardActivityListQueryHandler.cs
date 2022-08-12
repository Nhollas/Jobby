using AutoMapper;
using Jobby.Core.Dtos;
using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using Jobby.Core.Specifications;
using MediatR;

namespace Jobby.Core.Features.ActivityFeatures.Queries.GetBoardActivityList;
public class GetBoardActivityListQueryHandler : IRequestHandler<GetBoardActivityListQuery, List<ActivityDto>>
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

    public async Task<List<ActivityDto>> Handle(GetBoardActivityListQuery request, CancellationToken cancellationToken)
    {
        var activitySpec = new ActivityListFromBoardIdSpec(request.BoardId, _userId);

        var activityList = await _repository.ListAsync(activitySpec, cancellationToken);

        if (activityList is null)
        {
            return new List<ActivityDto>();
        }

        return _mapper.Map<List<ActivityDto>>(activityList);
    }
}
