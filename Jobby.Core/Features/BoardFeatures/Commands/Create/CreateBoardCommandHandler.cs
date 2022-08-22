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
        var board = new Board(
            Guid.NewGuid(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Name,
            defaultJobLists);

        var createdBoard = await _repository.AddAsync(board, cancellationToken);

        return createdBoard.Id;
    }

    private readonly List<JobList> defaultJobLists = new()
    {
        new JobList("Wishlist"),
        new JobList("Applied"),
        new JobList("Interview"),
        new JobList("Offer"),
        new JobList("Rejected")
    };
}
