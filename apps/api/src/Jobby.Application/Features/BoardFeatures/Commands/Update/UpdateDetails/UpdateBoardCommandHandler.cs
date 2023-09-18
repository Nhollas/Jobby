using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;

internal sealed class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, BaseResult<BoardDto, UpdateBoardOutcomes>>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public UpdateBoardCommandHandler(
        IRepository<Board> boardRepository,
        IUserService userService,
        IDateTimeProvider timeProvider, 
        IMapper mapper)
    {
        _boardRepository = boardRepository;
        _userId = userService.UserId();
        _timeProvider = timeProvider;
        _mapper = mapper;
    }

    public async Task<BaseResult<BoardDto, UpdateBoardOutcomes>> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        var boardResourceResult = await ResourceProvider<Board>
            .GetById(_boardRepository.GetByIdAsync)
            .Check(_userId, request.Id, cancellationToken);
        
        if (!boardResourceResult.IsSuccess)
        {
            return new BaseResult<BoardDto, UpdateBoardOutcomes>(
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

        return new BaseResult<BoardDto, UpdateBoardOutcomes>(
            IsSuccess: true,
            Outcome: UpdateBoardOutcomes.BoardUpdated,
            Response: _mapper.Map<BoardDto>(boardToUpdate)
        );
    }
}
