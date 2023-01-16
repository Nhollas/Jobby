using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.JobList;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Create;
internal sealed class CreateJobListCommandHandler : IRequestHandler<CreateJobListCommand, CreateJobListResponse>
{
    private readonly IRepository<JobList> _repository;
    private readonly IUserService _userService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _userId;

    public CreateJobListCommandHandler(
        IRepository<JobList> repository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
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
            0,
            request.BoardId);

        await _repository.AddAsync(createdJobList, cancellationToken);

        return new CreateJobListResponse(createdJobList.Id, createdJobList.Name);
    }
}
