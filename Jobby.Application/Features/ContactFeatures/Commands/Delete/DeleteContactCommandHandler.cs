using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Delete;

internal sealed class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Unit>
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

        if (contactToDelete is null)
        {
            throw new NotFoundException($"A contact with id {request.ContactId} could not be found.");
        }

        if (contactToDelete.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        await _repository.DeleteAsync(contactToDelete, cancellationToken);

        return Unit.Value;
    }
}
