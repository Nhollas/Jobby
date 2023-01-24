using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Job;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Application.Static;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Create;

internal sealed class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, CreateJobResponse>
{
    private readonly IReadRepository<Board> _boardRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateJobCommandHandler(
        IReadRepository<Board> boardRepository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IRepository<Job> jobRepository,
        IGuidProvider guidProvider)
    {
        _boardRepository = boardRepository;
        _userService = userService;
        _userId = _userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _jobRepository = jobRepository;
        _guidProvider = guidProvider;
    }

    public async Task<CreateJobResponse> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        Board board = await ResourceProvider<Board>
            .GetBySpec(_boardRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetBoardWithJobsSpecification(request.BoardId))
            .Check(_userId);

        if (!board.BoardOwnsJoblist(request.JobListId))
        {
            throw new NotFoundException($"The Board {request.BoardId} does not contain the JobList {request.JobListId}.");
        }

        JobList selectedJobList = board.JobLists.First(x => x.Id == request.JobListId);

        int newIndex;

        if (selectedJobList.Jobs.Count == 0)
        {
            newIndex = 0;
        }
        else
        {
            newIndex = selectedJobList.Jobs.Count;
        }

        var createdJob = Job.Create(
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Company,
            request.Colour,
            request.Title,
            newIndex,
            selectedJobList,
            board);

        await _jobRepository.AddAsync(createdJob, cancellationToken);

        return new CreateJobResponse(
            createdJob.Id,
            DateTimeFormatter.FormatDateTime(createdJob.CreatedDate),
            createdJob.LastUpdated,
            createdJob.Company,
            createdJob.Title,
            createdJob.Index);
    }
}
