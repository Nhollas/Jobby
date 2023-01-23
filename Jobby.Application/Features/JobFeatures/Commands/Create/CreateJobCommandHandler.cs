using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Job;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Application.Static;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Create;

internal sealed class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, CreateJobResponse>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateJobCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IRepository<Job> jobRepository,
        IGuidProvider guidProvider)
    {
        _boardRepository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _jobRepository = jobRepository;
        _guidProvider = guidProvider;
    }

    public async Task<CreateJobResponse> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var boardSpec = new GetBoardWithJobsSpec(request.BoardId);

        var board = await _boardRepository.FirstOrDefaultAsync(boardSpec, cancellationToken);

        if (board is null)
        {
            throw new NotFoundException($"The Board {request.BoardId} could not be found.");
        }

        if (board.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        if (!BoardOwnsJobList(board, request.JobListId))
        {
            throw new NotFoundException($"The board {request.BoardId} does not contain the JobList {request.JobListId}.");
        }

        JobList selectedJobList = board.JobList.Where(x => x.Id == request.JobListId).First();

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

    private static bool BoardOwnsJobList(Board board, Guid jobListId)
    {
        return board.JobList
            .Select(x => x.Id == jobListId)
            .Any();
    }
}
