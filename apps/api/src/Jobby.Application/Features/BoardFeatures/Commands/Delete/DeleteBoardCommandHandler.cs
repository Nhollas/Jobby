using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

internal class DeleteBoardCommandHandler(
    IRepository<Board> boardRepository,
    IContactRepository contactRepository,
    IUserService userService)
    : IRequestHandler<DeleteBoardCommand, IDispatchResult<DeleteBoardResponse>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<DeleteBoardResponse>> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        Board? board = await boardRepository.GetByReferenceAsync(request.BoardReference, cancellationToken);
        
        if (board is null)
        {
            return DispatchResults.NotFound<DeleteBoardResponse>(request.BoardReference);
        }
        
        if (board.OwnerId != _userId)
        {
            return DispatchResults.Unauthorized<DeleteBoardResponse>($"You are not authorised to access the resource {board.Reference}.");
        }

        await contactRepository.ClearBoardsAsync(board.Reference, cancellationToken);
        await boardRepository.DeleteAsync(board, cancellationToken);

        return DispatchResults.Ok(new DeleteBoardResponse());
    }
}
