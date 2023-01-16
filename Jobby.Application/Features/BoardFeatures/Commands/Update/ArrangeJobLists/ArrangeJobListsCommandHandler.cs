using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update.ArrangeJobLists;
internal sealed class ArrangeJobListsCommandHandler : IRequestHandler<ArrangeJobListsCommand, Unit>
{
    private readonly IRepository<Board> _repository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public ArrangeJobListsCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(ArrangeJobListsCommand request, CancellationToken cancellationToken)
    {
        var spec = new BoardWithJobListsSpec(request.BoardId);

        Board board = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (board == null)
        {
            throw new NotFoundException($"A Board with id {request.BoardId} could not be found.");
        }

        if (board.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        board.ArrangeJobLists(request.JobListIndexes);
        board.UpdateEntity(_timeProvider.UtcNow);

        await _repository.UpdateAsync(board, cancellationToken);

        return Unit.Value;
    }
}
