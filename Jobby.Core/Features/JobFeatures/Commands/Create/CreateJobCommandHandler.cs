using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using Jobby.Core.Specifications;
using MediatR;

namespace Jobby.Core.Features.JobFeatures.Commands.Create;

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Guid>
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
            // TODO: NotFound Problem Details.
        }

        if (BoardToLink.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        if (!BoardOwnsJobList(BoardToLink, request.JobListId))
        {
            // TODO: NotFound Problem Details.
        }

        Job createdJob = new(request.CompanyName, request.JobTitle);

        await LinkJobToBoardList(createdJob, BoardToLink, request.JobListId);

        return createdJob.Id;
    }

    private async Task LinkJobToBoardList(Job createdJob, Board board, Guid jobListId)
    {
        JobList selectedJobList =
            board.JobList
                .Where(x => x.Id == jobListId)
                .FirstOrDefault();

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
        if (board.JobList
            .Select(x => x.Id == jobListId)
            .Any())
        {
            return true;
        }

        return false;
    }
}
