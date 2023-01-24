using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;

internal sealed class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, Unit>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public UpdateBoardCommandHandler(
        IRepository<Board> boardRepository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _boardRepository = boardRepository;
        _userService = userService;
        _userId = _userService.UserId();
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        Board boardToUpdate = await ResourceProvider<Board>
            .GetById(_boardRepository.GetByIdAsync)
            .Check(_userId, request.Id);

        boardToUpdate.SetBoardName(request.Name);

        boardToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _boardRepository.UpdateAsync(boardToUpdate, cancellationToken);

        return Unit.Value;
    }
}
