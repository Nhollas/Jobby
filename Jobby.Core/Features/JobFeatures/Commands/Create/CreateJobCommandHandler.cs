using Jobby.Core.Entities.BoardAggregate;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.JobFeatures.Commands.Create;

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Guid>
{
    private readonly IRepository<Board> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateJobCommandHandler(
        IRepository<Board> repository,
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
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

        var sus = boardToUpdate.JobList.Where(x => x.Id == request.JobListId).FirstOrDefault();

        sus.Jobs.Add();
    }
}
