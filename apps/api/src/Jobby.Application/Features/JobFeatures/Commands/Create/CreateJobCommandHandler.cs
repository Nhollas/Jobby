using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Create;

internal class CreateJobCommandHandler(
    IReadRepository<Board> boardRepository,
    IUserService userService,
    TimeProvider timeProvider,
    IRepository<Job> jobRepository,
    IMapper mapper)
    : IRequestHandler<CreateJobCommand, IDispatchResult<JobDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<JobDto>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        Board? board = await boardRepository.FirstOrDefaultAsync(new GetBoardWithJobsSpecification(request.BoardReference), cancellationToken);

        if (board is null)
        {
            return DispatchResults.NotFound<JobDto>(request.BoardReference);
        }
        
        if (!board.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<JobDto>(request.BoardReference);
        }

        if (!board.BoardOwnsList(request.JobListReference))
        {
            return DispatchResults.BadRequest<JobDto>(request.JobListReference);
        }

        JobList selectedJobList = board.Lists.First(list => list.Reference == request.JobListReference);

        int newIndex = selectedJobList.Jobs.Count == 0 ? 0 : selectedJobList.Jobs.Count;

        Job createdJob = selectedJobList.CreateJob(
            timeProvider.GetUtcNow(),
            request.Company,
            request.Title);

        await jobRepository.AddAsync(createdJob, cancellationToken);

        
        return DispatchResults.Created(mapper.Map<JobDto>(createdJob));
    }
}
