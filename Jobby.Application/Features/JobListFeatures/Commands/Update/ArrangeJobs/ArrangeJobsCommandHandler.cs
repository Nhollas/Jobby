using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Features.JobListFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Update.ArrangeJobs;
internal sealed class ArrangeJobsCommandHandler : IRequestHandler<ArrangeJobsCommand, Unit>
{
    private readonly IRepository<JobList> _jobListRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly string _userId;

    public ArrangeJobsCommandHandler(
        IRepository<JobList> jobListRepository,
        IUserService userService,
        IDateTimeProvider timeProvider)
    {
        _jobListRepository = jobListRepository;
        _userId = userService.UserId();
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(ArrangeJobsCommand request, CancellationToken cancellationToken)
    {
        JobList jobList = await ResourceProvider<JobList>
            .GetBySpec(_jobListRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetJobListWithJobsSpecification(request.JobListId))
            .Check(_userId, cancellationToken);

        jobList.ArrangeJobs(request.JobIndexes);
        jobList.UpdateEntity(_timeProvider.UtcNow);

        await _jobListRepository.UpdateAsync(jobList, cancellationToken);

        return Unit.Value;
    }
}
