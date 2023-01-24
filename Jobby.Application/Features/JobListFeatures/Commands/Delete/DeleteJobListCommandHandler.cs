using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Delete;
internal sealed class DeleteJobListCommandHandler : IRequestHandler<DeleteJobListCommand, Unit>
{
    private readonly IRepository<JobList> _jobListRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteJobListCommandHandler(
        IRepository<JobList> jobListRepository,
        IUserService userService)
    {
        _jobListRepository = jobListRepository;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<Unit> Handle(DeleteJobListCommand request, CancellationToken cancellationToken)
    {
        JobList jobListToDelete = await ResourceProvider<JobList>
            .GetById(_jobListRepository.GetByIdAsync)
            .Check(_userId, request.Id);

        await _jobListRepository.DeleteAsync(jobListToDelete, cancellationToken);

        return Unit.Value;
    }
}
