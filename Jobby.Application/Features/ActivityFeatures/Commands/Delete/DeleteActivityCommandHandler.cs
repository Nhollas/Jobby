using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Delete;

internal sealed class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Unit>
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

        if (activityToDelete is null)
        {
            throw new NotFoundException($"An activity with id {request.ActivityId} could not be found.");
        }

        if (activityToDelete.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        await _repository.DeleteAsync(activityToDelete, cancellationToken);

        return Unit.Value;
    }
}
