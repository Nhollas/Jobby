using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Update.ArrangeJobs;
internal sealed class ArrangeJobsCommandHandler : IRequestHandler<ArrangeJobsCommand, Unit>
{
    private readonly IRepository<JobList> _repository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public ArrangeJobsCommandHandler(
        IRepository<JobList> repository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(ArrangeJobsCommand request, CancellationToken cancellationToken)
    {
        var spec = new ListWithJobsSpec(request.JobListId);

        JobList jobList = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (jobList == null)
        {
            throw new NotFoundException($"A jobList with id {request.JobListId} could not be found.");
        }

        if (jobList.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        jobList.ArrangeJobs(request.JobIndexes);
        jobList.UpdateEntity(_timeProvider.UtcNow);

        await _repository.UpdateAsync(jobList, cancellationToken);

        return Unit.Value;
    }
}
