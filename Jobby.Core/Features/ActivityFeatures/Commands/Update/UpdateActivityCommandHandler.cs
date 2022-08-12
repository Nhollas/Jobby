using AutoMapper;
using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.ActivityFeatures.Commands.Update;

public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Unit>
{
    private readonly IRepository<Activity> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public UpdateActivityCommandHandler(
        IRepository<Activity> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        Activity activityToUpdate = await _repository.GetByIdAsync(request.ActivityId, cancellationToken);

        if (activityToUpdate == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (activityToUpdate.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        _mapper.Map(request, activityToUpdate, typeof(UpdateActivityCommand), typeof(Activity));

        await _repository.UpdateAsync(activityToUpdate, cancellationToken);

        return Unit.Value;
    }
}
