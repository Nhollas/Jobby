using Jobby.Application.Abstractions.Messaging;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

internal sealed class CreateBoardCommandHandler : ICommandHandler<CreateBoardCommand, CreateBoardResponse>
{
    private readonly IRepository<Board> _repository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateBoardCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<CreateBoardResponse> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        Guid BoardId = Guid.NewGuid();

        List<JobList> defaultJobLists = new()
        {
            JobList.Create(Guid.NewGuid(), _dateTimeProvider.UtcNow, _userId, "Applied", 0, BoardId),
            JobList.Create(Guid.NewGuid(), _dateTimeProvider.UtcNow, _userId, "Wishlist", 1, BoardId),
            JobList.Create(Guid.NewGuid(), _dateTimeProvider.UtcNow, _userId, "Interview", 2, BoardId),
            JobList.Create(Guid.NewGuid(), _dateTimeProvider.UtcNow, _userId, "Offer", 3, BoardId),
            JobList.Create(Guid.NewGuid(), _dateTimeProvider.UtcNow, _userId, "Rejected", 4, BoardId),
        };

        var board = Board.Create(
            BoardId,
            _dateTimeProvider.UtcNow,
            _userId,
            request.Name,
            defaultJobLists);

        await _repository.AddAsync(board, cancellationToken);

        return new CreateBoardResponse(
            board.Id,
            board.Name,
            board.CreatedDate);
    }
}
