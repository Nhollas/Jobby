using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update.ArrangeJobLists;
internal sealed class ArrangeJobListsCommandHandler : IRequestHandler<ArrangeJobListsCommand, Unit>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public ArrangeJobListsCommandHandler(
        IRepository<Board> boardRepository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _boardRepository = boardRepository;
        _userService = userService;
        _userId = _userService.UserId();
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(ArrangeJobListsCommand request, CancellationToken cancellationToken)
    {
        Board board = await ResourceProvider<Board>
            .GetBySpec(_boardRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetBoardWithJobListsSpecification(request.BoardId))
            .Check(_userId);

        board.ArrangeJobLists(request.JobListIndexes);
        board.UpdateEntity(_timeProvider.UtcNow);

        await _boardRepository.UpdateAsync(board, cancellationToken);

        return Unit.Value;
    }
}
