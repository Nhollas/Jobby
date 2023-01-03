using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Update.ChangeJobListOrder;

internal sealed class ChangeJobListOrderCommandHandler : IRequestHandler<ChangeJobListOrderCommand, Unit>
{
    private readonly IRepository<Board> _repository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public ChangeJobListOrderCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(ChangeJobListOrderCommand request, CancellationToken cancellationToken)
    {
        var spec = new GetBoardByIdSpec(request.BoardId);
        Board boardToUpdate = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (boardToUpdate == null)
        {
            throw new NotFoundException($"A board with id {request.BoardId} could not be found.");
        }

        if (boardToUpdate.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        boardToUpdate.ChangeJobListPosition(request.JobListId, request.TargetIndex);

        boardToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _repository.UpdateAsync(boardToUpdate, cancellationToken);

        return Unit.Value;
    }
}
