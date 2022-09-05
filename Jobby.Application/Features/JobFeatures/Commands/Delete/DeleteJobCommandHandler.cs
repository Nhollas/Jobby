using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Delete;

internal sealed class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Unit>
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

        if (jobToDelete is null)
        {
            throw new NotFoundException($"The Job {request.JobId} could not be found.");
        }

        if (jobToDelete.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        await _repository.DeleteAsync(jobToDelete, cancellationToken);

        return Unit.Value;
    }
}
