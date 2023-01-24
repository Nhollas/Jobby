using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

internal sealed class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, Unit>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteBoardCommandHandler(
        IRepository<Board> boardRepository,
        IUserService userService)
    {
        _boardRepository = boardRepository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        Board boardToDelete = await ResourceProvider<Board>
            .GetById(_boardRepository.GetByIdAsync)
            .Check(_userId, request.BoardId);  

        await _boardRepository.DeleteAsync(boardToDelete, cancellationToken);

        return Unit.Value;
    }
}
