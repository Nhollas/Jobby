using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

internal sealed class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, Unit>
{
    private readonly IRepository<Board> _repository;
    private readonly IRepository<Contact> _contactRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteBoardCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IRepository<Contact> contactRepository)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _contactRepository = contactRepository;
    }

    public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        Board boardToDelete = await _repository.GetByIdAsync(request.BoardId, cancellationToken);

        if (boardToDelete is null)
        {
            throw new NotFoundException($"A board with id {request.BoardId} could not be found.");
        }

        if (boardToDelete.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        var contactSpec = new ListBoardContactsMiniSpec(request.BoardId);

        var contacts = await _contactRepository.ListAsync(contactSpec, cancellationToken);

        if (contacts.Count > 0)
        {
            await _contactRepository.DeleteRangeAsync(contacts, cancellationToken);
        }

        await _repository.DeleteAsync(boardToDelete, cancellationToken);

        return Unit.Value;
    }
}
