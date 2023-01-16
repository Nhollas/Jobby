using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Delete;
internal sealed class DeleteJobListCommandHandler : IRequestHandler<DeleteJobListCommand, Unit>
{
    private readonly IRepository<JobList> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteJobListCommandHandler(
        IRepository<JobList> repository,
        IUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteJobListCommand request, CancellationToken cancellationToken)
    {
        JobList jobListToDelete = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (jobListToDelete is null)
        {
            throw new NotFoundException($"A JobList with id {request.Id} could not be found.");
        }

        if (jobListToDelete.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        await _repository.DeleteAsync(jobListToDelete, cancellationToken);

        return Unit.Value;
    }
}
