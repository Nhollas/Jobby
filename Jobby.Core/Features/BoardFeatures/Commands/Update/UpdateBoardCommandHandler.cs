using Jobby.Core.Entities.BoardAggregate;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.Update;

public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, Unit>
{
    private readonly IRepository<Board> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public UpdateBoardCommandHandler(
        IRepository<Board> repository, 
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        Board boardToUpdate = await _repository.GetByIdAsync(request.BoardId, cancellationToken);

        if (boardToUpdate == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (boardToUpdate.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        boardToUpdate.Name = request.BoardName;

        return Unit.Value;
    }
}
