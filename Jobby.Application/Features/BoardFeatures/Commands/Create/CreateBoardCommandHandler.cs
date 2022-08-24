using Jobby.Application.Abstractions.Messaging;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces;
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
            new JobList("Wishlist", _dateTimeProvider.UtcNow, _userId),
            new JobList("Applied", _dateTimeProvider.UtcNow, _userId),
            new JobList("Interview", _dateTimeProvider.UtcNow, _userId),
            new JobList("Offer", _dateTimeProvider.UtcNow, _userId),
            new JobList("Rejected", _dateTimeProvider.UtcNow, _userId),
        };

        var board = Board.Create(
            _dateTimeProvider.UtcNow,
            _userId,
            request.Name,
            defaultJobLists);

        await _repository.AddAsync(board, cancellationToken);

        return board.Id;
    }
}
