using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Create;

internal sealed class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Guid>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateJobCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IRepository<Job> jobRepository)
    {
        _boardRepository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _jobRepository = jobRepository;
    }

    /*
        A Job is created by it being added to a JobList inside board entity (parent).
    */
    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var boardSpec = new IncludeJobListSpecification(request.BoardId);

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

        var createdJob = Job.Create(
            Guid.NewGuid(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Company,
            request.Title,
            selectedJobList);

        await _jobRepository.AddAsync(createdJob, cancellationToken);

        return createdJob.Id;
    }

    private static bool BoardOwnsJobList(Board board, Guid jobListId)
    {
        return board.JobList
            .Select(x => x.Id == jobListId)
            .Any();
    }
}
