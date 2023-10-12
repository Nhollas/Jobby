using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
internal sealed class MoveJobCommandHandler : IRequestHandler<MoveJobCommand, BaseResult<MoveJobResponse, MoveJobOutcomes>>
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

    public async Task<BaseResult<MoveJobResponse, MoveJobOutcomes>> Handle(MoveJobCommand request, CancellationToken cancellationToken)
    {
        var jobResourceResult = await ResourceProvider<Job>
            .GetByReference(_jobRepository.GetByReferenceAsync)
            .Check(_userId, request.JobReference, cancellationToken);

        if (!jobResourceResult.IsSuccess)
        {
            return new BaseResult<MoveJobResponse, MoveJobOutcomes>(
                IsSuccess: false,
                Outcome: jobResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => MoveJobOutcomes.UnauthorizedJobAccess,
                    Outcome.NotFound => MoveJobOutcomes.UnknownJob,
                    _ => MoveJobOutcomes.UnknownError
                },
                ErrorMessage: jobResourceResult.ErrorMessage
            );
        }

        var jobToMove = jobResourceResult.Response;

        // TODO: Fetch the target job list and set it on the job
        // jobToMove.SetJobList(request.TargetJobListId);
        jobToMove.UpdateEntity(_timeProvider.UtcNow);

        await _jobRepository.UpdateAsync(jobToMove, cancellationToken);

        return new BaseResult<MoveJobResponse, MoveJobOutcomes>(
            IsSuccess: true,
            Outcome: MoveJobOutcomes.JobMoved,
            Response: new MoveJobResponse()
        );
    }
}
