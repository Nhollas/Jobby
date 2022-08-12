using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.DeleteJobList;
public class DeleteJobListCommandHandler : IRequestHandler<DeleteJobListCommand, Unit>
{
    private readonly IRepository<Board> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteJobListCommandHandler(
        IRepository<Board> repository,
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteJobListCommand request, CancellationToken cancellationToken)
    {
        Board board = await _repository.GetByIdAsync(request.BoardId, cancellationToken);

        if (board == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (board.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        if (!board.JobList.Select(x => x.Id == request.ListId).FirstOrDefault())
        {
            // TODO: NotFound Problem Details.
        }

        board.JobList.Remove(board.JobList.Single(s => s.Id == request.ListId));

        await _repository.UpdateAsync(board, cancellationToken);

        return Unit.Value;
    }
}
