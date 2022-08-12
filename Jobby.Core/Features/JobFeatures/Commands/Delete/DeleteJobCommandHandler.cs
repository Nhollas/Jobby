using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.JobFeatures.Commands.Delete;

public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Unit>
{
    private readonly IRepository<Job> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteJobCommandHandler(
        IRepository<Job> repository,
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        Job jobToDelete = await _repository.GetByIdAsync(request.JobId, cancellationToken);

        if (jobToDelete == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (jobToDelete.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        await _repository.DeleteAsync(jobToDelete, cancellationToken);

        return Unit.Value;
    }
}
