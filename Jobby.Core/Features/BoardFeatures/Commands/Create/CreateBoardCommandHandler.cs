using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.Create;

public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, Guid>
{
    private readonly IRepository<Board> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateBoardCommandHandler(
        IRepository<Board> repository,
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Guid> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var createdBoard = await _repository.AddAsync(new Board(request.Name, _userId, defaultJobLists), cancellationToken);

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
