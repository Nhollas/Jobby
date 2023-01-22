using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.JobList;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Create;
internal sealed class CreateJobListCommandHandler : IRequestHandler<CreateJobListCommand, CreateJobListResponse>
{
    private readonly IRepository<JobList> _repository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IUserService _userService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _userId;

    public CreateJobListCommandHandler(
        IRepository<JobList> repository,
        IRepository<Job> jobRepository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _jobRepository = jobRepository;
        _userService = userService;
        _userId = _userService.UserId();
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<CreateJobListResponse> Handle(CreateJobListCommand request, CancellationToken cancellationToken)
    {
        var createdJobList = JobList.Create(
            Guid.NewGuid(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Name,
            request.Index,
            request.BoardId);

        await _repository.AddAsync(createdJobList, cancellationToken);

        if (request.InitJobId != Guid.Empty)
        {
            var jobToUpdate = await _jobRepository.GetByIdAsync(request.InitJobId, cancellationToken);

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
