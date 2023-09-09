using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;

internal sealed class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, BaseResult<UpdateBoardResponse, UpdateBoardOutcomes>>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly string _userId;

    public UpdateBoardCommandHandler(
        IRepository<Board> boardRepository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _boardRepository = boardRepository;
        _userId = userService.UserId();
        _timeProvider = timeProvider;
    }

    public async Task<BaseResult<UpdateBoardResponse, UpdateBoardOutcomes>> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        var boardResourceResult = await ResourceProvider<Board>
            .GetById(_boardRepository.GetByIdAsync)
            .Check(_userId, request.Id, cancellationToken);
        
        if (!boardResourceResult.IsSuccess)
        {
            return new BaseResult<UpdateBoardResponse, UpdateBoardOutcomes>(
                IsSuccess: false,
                Outcome: boardResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => UpdateBoardOutcomes.UnauthorizedBoardAccess,
                    Outcome.NotFound => UpdateBoardOutcomes.UnknownBoard,
                    _ => UpdateBoardOutcomes.UnknownError
                },
                ErrorMessage: boardResourceResult.ErrorMessage
            );
        }
        
        var boardToUpdate = boardResourceResult.Response;
        
        boardToUpdate.SetBoardName(request.Name);

        boardToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _boardRepository.UpdateAsync(boardToUpdate, cancellationToken);

        return new BaseResult<UpdateBoardResponse, UpdateBoardOutcomes>(
            IsSuccess: true,
            Outcome: UpdateBoardOutcomes.BoardUpdated,
            Response: new UpdateBoardResponse()
        );
    }
}
