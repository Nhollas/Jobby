using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.AddJobList;
public class AddJobListCommandHandler : IRequestHandler<AddJobListCommand, Unit>
{
    private readonly IRepository<Board> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public AddJobListCommandHandler(
        IRepository<Board> repository,
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(AddJobListCommand request, CancellationToken cancellationToken)
    {
        var boardToUpdate = await _repository.GetByIdAsync(request.BoardId, cancellationToken);

        if (boardToUpdate == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (boardToUpdate.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        JobList mockJobList = new("List Name");

        boardToUpdate.JobList.Add(mockJobList);

        await _repository.UpdateAsync(boardToUpdate, cancellationToken);

        return Unit.Value;
    }
}
