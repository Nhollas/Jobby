using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
internal sealed class MoveJobCommandHandler : IRequestHandler<MoveJobCommand, Unit>
{
    private readonly IRepository<Job> _jobRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly string _userId;

    public MoveJobCommandHandler(
        IRepository<Job> jobRepository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _jobRepository = jobRepository;
        _userId = userService.UserId();
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(MoveJobCommand request, CancellationToken cancellationToken)
    {
        Job jobToMove = await ResourceProvider<Job>
            .GetById(_jobRepository.GetByIdAsync)
            .Check(_userId, request.JobId, cancellationToken);

        jobToMove.SetJobList(request.TargetJobListId);
        jobToMove.UpdateEntity(_timeProvider.UtcNow);

        await _jobRepository.UpdateAsync(jobToMove, cancellationToken);

        return Unit.Value;
    }
}
