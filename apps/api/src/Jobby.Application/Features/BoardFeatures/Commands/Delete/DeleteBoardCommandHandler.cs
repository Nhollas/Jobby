using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

internal sealed class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, BaseResult<DeleteBoardResponse, DeleteBoardOutcomes>>
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

    public async Task<BaseResult<DeleteBoardResponse, DeleteBoardOutcomes>> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        var boardResourceResult = await ResourceProvider<Board>
            .GetByReference(_boardRepository.GetByReferenceAsync)
            .Check(_userId, request.BoardReference, cancellationToken);
        
        if (!boardResourceResult.IsSuccess)
        {
            return new BaseResult<DeleteBoardResponse, DeleteBoardOutcomes>(
                IsSuccess: false,
                Outcome: boardResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => DeleteBoardOutcomes.UnauthorizedBoardAccess,
                    Outcome.NotFound => DeleteBoardOutcomes.UnknownBoard,
                    _ => DeleteBoardOutcomes.UnknownError
                },
                ErrorMessage: boardResourceResult.ErrorMessage
            );
        }

        await _contactRepository.ClearBoardsAsync(request.BoardReference, cancellationToken);
        
        var boardToDelete = boardResourceResult.Response;
        
        await _boardRepository.DeleteAsync(boardToDelete, cancellationToken);

        return new BaseResult<DeleteBoardResponse, DeleteBoardOutcomes>(
            IsSuccess: true,
            Outcome: DeleteBoardOutcomes.BoardDeleted,
            Response: new DeleteBoardResponse()
        );
    }
}
