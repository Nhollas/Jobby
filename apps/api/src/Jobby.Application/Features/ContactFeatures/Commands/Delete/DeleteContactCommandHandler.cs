using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Delete;

internal sealed class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Unit>
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly string _userId;

    public DeleteContactCommandHandler(
        IRepository<Contact> contactRepository,
        IUserService userService)
    {
        _contactRepository = contactRepository;
        _userId = userService.UserId();
    }

    public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        Contact contactToDelete = await ResourceProvider<Contact>
            .GetById(_contactRepository.GetByIdAsync)
            .Check(_userId, request.ContactId, cancellationToken);

        await _contactRepository.DeleteAsync(contactToDelete, cancellationToken);

        return Unit.Value;
    }
}
