using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses.Common;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

internal sealed class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BaseResult<CreateBoardResponse, CreateBoardOutcomes>>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IGuidProvider _guidProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _userId;

    public CreateBoardCommandHandler(
        IRepository<Board> boardRepository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider)
    {
        _boardRepository = boardRepository;
        _userId = userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
    }

    public async Task<BaseResult<CreateBoardResponse, CreateBoardOutcomes>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        Guid boardId = _guidProvider.Create();

        List<JobList> defaultJobLists = new()
        {
            JobList.Create(_guidProvider.Create(), _dateTimeProvider.UtcNow, _userId, "Applied", 0, boardId),
            JobList.Create(_guidProvider.Create(), _dateTimeProvider.UtcNow, _userId, "Wishlist", 1, boardId),
            JobList.Create(_guidProvider.Create(), _dateTimeProvider.UtcNow, _userId, "Interview", 2, boardId),
            JobList.Create(_guidProvider.Create(), _dateTimeProvider.UtcNow, _userId, "Offer", 3, boardId),
            JobList.Create(_guidProvider.Create(), _dateTimeProvider.UtcNow, _userId, "Rejected", 4, boardId),
        };

        var board = Board.Create(
            boardId,
            _dateTimeProvider.UtcNow,
            _userId,
            request.Name,
            defaultJobLists);

        await _boardRepository.AddAsync(board, cancellationToken);

        return new BaseResult<CreateBoardResponse, CreateBoardOutcomes>(
            IsSuccess: true,
            Outcome: CreateBoardOutcomes.BoardCreated,
            Response: new CreateBoardResponse(boardId, board.Name, board.CreatedDate));
    }
}
