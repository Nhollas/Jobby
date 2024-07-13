using AutoMapper;
using Jobby.Application.Dtos;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ListFeatures.Commands.Create;

internal class CreateListCommandHandler(
    IRepository<JobList> jobListRepository,
    IRepository<Job> jobRepository,
    IUserService userService,
    TimeProvider timeProvider,
    IRepository<Board> boardRepository,
    IMapper mapper)
    : IRequestHandler<CreateListCommand, IDispatchResult<JobListDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<JobListDto>> Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        Board? board = await boardRepository.GetByReferenceAsync(request.BoardReference, cancellationToken);
        
        if (board is null)
        {
            return DispatchResults.NotFound<JobListDto>(request.BoardReference);
        }
        
        if (!board.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<JobListDto>(board.Reference);
        }
        
        JobList createdJobList = board.AddJobList(
            timeProvider,
            request.Name);

        await jobListRepository.AddAsync(createdJobList, cancellationToken);

        if (request.JobReference == string.Empty)
        {
            return DispatchResults.Ok(mapper.Map<JobListDto>(createdJobList));
        }
        
        Job? job = await jobRepository.GetByReferenceAsync(request.JobReference, cancellationToken);
        
        if (job is null)
        {
            return DispatchResults.NotFound<JobListDto>(request.JobReference);
        }
        
        if (!job.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<JobListDto>(job.Reference);
        }
        
        job.SetJobList(createdJobList);
        job.SetIndex(0);
        
        await jobRepository.UpdateAsync(job, cancellationToken);
        
        return DispatchResults.Ok(mapper.Map<JobListDto>(createdJobList));
    }
}
