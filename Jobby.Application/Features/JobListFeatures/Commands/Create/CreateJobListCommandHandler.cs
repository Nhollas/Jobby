using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.JobList;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Create;
internal sealed class CreateJobListCommandHandler : IRequestHandler<CreateJobListCommand, CreateJobListResponse>
{
    private readonly IRepository<JobList> _jobListRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IGuidProvider _guidProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _userId;

    public CreateJobListCommandHandler(
        IRepository<JobList> jobListRepository,
        IRepository<Job> jobRepository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider)
    {
        _jobListRepository = jobListRepository;
        _jobRepository = jobRepository;
        _userId = userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
    }

    public async Task<CreateJobListResponse> Handle(CreateJobListCommand request, CancellationToken cancellationToken)
    {
        var createdJobList = JobList.Create(
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Name,
            request.Index,
            request.BoardId);

        await _jobListRepository.AddAsync(createdJobList, cancellationToken);

        if (request.InitJobId != Guid.Empty)
        {
            Job jobToUpdate = await ResourceProvider<Job>
                .GetById(_jobRepository.GetByIdAsync)
                .Check(_userId, request.InitJobId, cancellationToken);

            jobToUpdate.SetJobList(createdJobList.Id);
            jobToUpdate.SetIndex(0);

            await _jobRepository.UpdateAsync(jobToUpdate, cancellationToken);
        }

        return new CreateJobListResponse
        {
            Id = createdJobList.Id,
            CreatedDate = createdJobList.CreatedDate,
            Index = createdJobList.Index,
            LastUpdated = createdJobList.LastUpdated,
            Name = createdJobList.Name  
        };
    }
}
