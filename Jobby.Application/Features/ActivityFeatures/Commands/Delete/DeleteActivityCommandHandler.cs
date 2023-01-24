using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Delete;

internal sealed class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Unit>
{
    private readonly IRepository<Activity> _activityRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteActivityCommandHandler(
        IRepository<Activity> activityRepository,
        IUserService userService)
    {
        _activityRepository = activityRepository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        Activity activityToDelete = await ResourceProvider<Activity>
            .GetById(_activityRepository.GetByIdAsync)
            .Check(_userId, request.ActivityId);

        await _activityRepository.DeleteAsync(activityToDelete, cancellationToken);

        return Unit.Value;
    }
}
