using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

internal sealed class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, Unit>
{
    private readonly IRepository<Board> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteBoardCommandHandler(
        IRepository<Board> repository,
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        Board boardToDelete = await _repository.GetByIdAsync(request.BoardId, cancellationToken);

        if (boardToDelete is null)
        {
            throw new NotFoundException($"A board with id {request.BoardId} could not be found.");
        }

        if (boardToDelete.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        await _repository.DeleteAsync(boardToDelete, cancellationToken);

        return Unit.Value;
    }
}
