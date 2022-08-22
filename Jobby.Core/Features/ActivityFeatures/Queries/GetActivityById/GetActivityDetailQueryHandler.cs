using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Queries.GetActivityById;
internal sealed class GetActivityDetailQueryHandler : IRequestHandler<GetActivityDetailQuery, ActivityDto>
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
            throw new NotFoundException($"An activity with id {request.ActivityId} could not be found.");
        }

        if (activityToGet.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        return _mapper.Map<ActivityDto>(activityToGet);
    }
}
