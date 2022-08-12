using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.Delete;

public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, Unit>
{
    private readonly IRepository<Board> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteBoardCommandHandler(
        IRepository<Board> repository,
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        Board boardToDelete = await _repository.GetByIdAsync(request.BoardId, cancellationToken);

        if (boardToDelete == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (boardToDelete.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        await _repository.DeleteAsync(boardToDelete, cancellationToken);

        return Unit.Value;
    }
}
