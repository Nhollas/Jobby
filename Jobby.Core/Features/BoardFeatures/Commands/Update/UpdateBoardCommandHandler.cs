using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update;

internal sealed class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, Unit>
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
            throw new NotFoundException($"A board with id {request.BoardId} could not be found.");
        }

        if (boardToUpdate.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        boardToUpdate.Name = request.BoardName;

        await _repository.UpdateAsync(boardToUpdate, cancellationToken);

        return Unit.Value;
    }
}
