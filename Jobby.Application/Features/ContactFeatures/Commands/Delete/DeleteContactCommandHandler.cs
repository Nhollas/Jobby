using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Delete;

internal sealed class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Unit>
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteContactCommandHandler(
        IRepository<Contact> contactRepository,
        IUserService userService)
    {
        _contactRepository = contactRepository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        Contact contactToDelete = await ResourceProvider<Contact>
            .GetById(_contactRepository.GetByIdAsync)
            .Check(_userId, request.ContactId);

        await _contactRepository.DeleteAsync(contactToDelete, cancellationToken);

        return Unit.Value;
    }
}
