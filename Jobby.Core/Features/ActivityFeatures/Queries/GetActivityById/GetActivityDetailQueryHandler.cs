using AutoMapper;
using Jobby.Core.Dtos;
using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.ActivityFeatures.Queries.GetActivityById;
public class GetActivityDetailQueryHandler : IRequestHandler<GetActivityDetailQuery, ActivityDto>
{
    private readonly IRepository<Activity> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetActivityDetailQueryHandler(
        IRepository<Activity> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<ActivityDto> Handle(GetActivityDetailQuery request, CancellationToken cancellationToken)
    {
        var activityToGet = await _repository.GetByIdAsync(request.ActivityId, cancellationToken);

        if (activityToGet == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (activityToGet.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        return _mapper.Map<ActivityDto>(activityToGet);
    }
}
