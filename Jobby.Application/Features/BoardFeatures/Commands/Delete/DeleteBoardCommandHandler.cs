using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

internal sealed class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, Unit>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IContactRepository _contactRepository;
    private readonly string _userId;

    public DeleteBoardCommandHandler(
        IRepository<Board> boardRepository,
        IContactRepository contactRepository,
        IUserService userService)
    {
        _boardRepository = boardRepository;
        _contactRepository = contactRepository;
        _userId = userService.UserId();
    }

    public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        Board boardToDelete = await ResourceProvider<Board>
            .GetById(_boardRepository.GetByIdAsync)
            .Check(_userId, request.BoardId, cancellationToken);

        await _contactRepository.ClearBoardsAsync(request.BoardId, cancellationToken);

        await _boardRepository.DeleteAsync(boardToDelete, cancellationToken);

        return Unit.Value;
    }
}
