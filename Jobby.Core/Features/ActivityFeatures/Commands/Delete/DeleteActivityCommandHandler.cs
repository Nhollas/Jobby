using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.ActivityFeatures.Commands.Delete;

public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Unit>
{
    private readonly IRepository<Activity> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteActivityCommandHandler(
        IRepository<Activity> repository,
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        Activity activityToDelete = await _repository.GetByIdAsync(request.ActivityId, cancellationToken);

        if (activityToDelete == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (activityToDelete.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        await _repository.DeleteAsync(activityToDelete, cancellationToken);

        return Unit.Value;
    }
}
