using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Delete;

internal sealed class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Unit>
{
    private readonly IRepository<Job> _jobRepository;
    private readonly string _userId;

    public DeleteJobCommandHandler(
        IRepository<Job> jobRepository,
        IUserService userService)
    {
        _jobRepository = jobRepository;
        _userId = userService.UserId();
    }

    public async Task<Unit> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        Job jobToDelete = await ResourceProvider<Job>
            .GetById(_jobRepository.GetByIdAsync)
            .Check(_userId, request.JobId, cancellationToken);

        await _jobRepository.DeleteAsync(jobToDelete, cancellationToken);

        return Unit.Value;
    }
}
