using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.ContactFeatures.Commands.Delete;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Unit>
{
    private readonly IRepository<Contact> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteContactCommandHandler(
        IRepository<Contact> repository,
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        Contact contactToDelete = await _repository.GetByIdAsync(request.ContactId, cancellationToken);

        if (contactToDelete == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (contactToDelete.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        await _repository.DeleteAsync(contactToDelete, cancellationToken);

        return Unit.Value;
    }
}
