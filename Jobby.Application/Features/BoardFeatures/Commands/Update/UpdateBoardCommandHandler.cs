using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update;

internal sealed class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, Unit>
{
    private readonly IRepository<Board> _repository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public UpdateBoardCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        Board boardToUpdate = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (boardToUpdate == null)
        {
            throw new NotFoundException($"A board with id {request.Id} could not be found.");
        }

        if (boardToUpdate.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        boardToUpdate.SetBoardName(request.Name);

        boardToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _repository.UpdateAsync(boardToUpdate, cancellationToken);

        return Unit.Value;
    }
}
