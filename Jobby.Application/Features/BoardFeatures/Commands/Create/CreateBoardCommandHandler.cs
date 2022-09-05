using Jobby.Application.Abstractions.Messaging;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

internal sealed class CreateBoardCommandHandler : ICommandHandler<CreateBoardCommand, Guid>
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

    public async Task<Guid> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        List<JobList> defaultJobLists = new()
        {
            new JobList(Guid.NewGuid(), _dateTimeProvider.UtcNow, _userId, "Wishlist"),
            new JobList(Guid.NewGuid(), _dateTimeProvider.UtcNow, _userId, "Applied"),
            new JobList(Guid.NewGuid(), _dateTimeProvider.UtcNow, _userId, "Interview"),
            new JobList(Guid.NewGuid(), _dateTimeProvider.UtcNow, _userId, "Offer"),
            new JobList(Guid.NewGuid(), _dateTimeProvider.UtcNow, _userId, "Rejected"),
        };

        var board = Board.Create(
            Guid.NewGuid(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Name,
            defaultJobLists);

        await _repository.AddAsync(board, cancellationToken);

        return board.Id;
    }
}
