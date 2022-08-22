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
    private readonly IRepository<JobList> _jobListRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateJobCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IRepository<JobList> jobListRepository)
    {
        _boardRepository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _jobListRepository = jobListRepository;
    }

    /*
        A Job is created by it being added to a JobList inside board entity (parent).
    */
    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var boardSpec = new IncludeJobListSpecification(request.BoardId);

        var BoardToLink = await _boardRepository.FirstOrDefaultAsync(boardSpec, cancellationToken);

        if (BoardToLink is null)
        {
            throw new NotFoundException($"The Board {request.BoardId} could not be found.");
        }

        if (BoardToLink.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        if (!BoardOwnsJobList(BoardToLink, request.JobListId))
        {
            throw new NotFoundException($"The board {request.BoardId} does not contain the JobList {request.JobListId}.");
        }

        Job createdJob = new(request.CompanyName, request.JobTitle);

        await LinkJobToBoardList(createdJob, BoardToLink, request.JobListId);

        return createdJob.Id;
    }

    private async Task LinkJobToBoardList(Job createdJob, Board board, Guid jobListId)
    {
        JobList selectedJobList =
            board.JobList.FirstOrDefault(x => x.Id == jobListId);

        if (selectedJobList.Jobs is null)
        {
            selectedJobList.Jobs = new List<Job>
            {
                createdJob
            };
        }
        else
        {
            selectedJobList.Jobs.Add(createdJob);
        }

        await _jobListRepository.UpdateAsync(selectedJobList);
    }

    private static bool BoardOwnsJobList(Board board, Guid jobListId)
    {
        return board.JobList
            .Select(x => x.Id == jobListId)
            .Any();
    }
}
