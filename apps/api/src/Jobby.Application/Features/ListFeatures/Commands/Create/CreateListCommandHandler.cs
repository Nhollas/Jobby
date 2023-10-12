using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ListFeatures.Commands.Create;
internal sealed class CreateListCommandHandler : IRequestHandler<CreateListCommand, BaseResult<JobListDto, CreateListOutcomes>>
{
    private readonly IRepository<JobList> _jobListRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IRepository<Board> _boardRepository;
    private readonly IGuidProvider _guidProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _userId;

    public CreateListCommandHandler(
        IRepository<JobList> jobListRepository,
        IRepository<Job> jobRepository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider, 
        IRepository<Board> boardRepository)
    {
        _jobListRepository = jobListRepository;
        _jobRepository = jobRepository;
        _userId = userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
        _boardRepository = boardRepository;
    }

    public async Task<BaseResult<JobListDto, CreateListOutcomes>> Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        var boardResourceResult = await ResourceProvider<Board>
            .GetByReference(_boardRepository.GetByReferenceAsync)
            .Check(_userId, request.BoardReference, cancellationToken);

        if (!boardResourceResult.IsSuccess)
        {
            return new BaseResult<JobListDto, CreateListOutcomes>(
                IsSuccess: false,
                Outcome: boardResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => CreateListOutcomes.UnauthorizedBoardAccess,
                    Outcome.NotFound => CreateListOutcomes.UnknownBoard,
                    _ => CreateListOutcomes.UnknownError
                },
                ErrorMessage: boardResourceResult.ErrorMessage
            );
        }
        
        var board = boardResourceResult.Response;
        
        var createdJobList = JobList.Create(
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Name,
            request.Index,
            board);

        await _jobListRepository.AddAsync(createdJobList, cancellationToken);

        if (request.JobReference != string.Empty)
        {
            var jobResourceResult = await ResourceProvider<Job>
                .GetByReference(_jobRepository.GetByReferenceAsync)
                .Check(_userId, request.JobReference, cancellationToken);
            
            if (!jobResourceResult.IsSuccess)
            {
                return new BaseResult<JobListDto, CreateListOutcomes>(
                    IsSuccess: false,
                    Outcome: jobResourceResult.Outcome switch
                    {
                        Outcome.Unauthorised => CreateListOutcomes.UnauthorizedJobAccess,
                        Outcome.NotFound => CreateListOutcomes.UnknownJob,
                        _ => CreateListOutcomes.UnknownError
                    },
                    ErrorMessage: jobResourceResult.ErrorMessage
                );
            }
            
            var jobToUpdate = jobResourceResult.Response;

            jobToUpdate.SetJobList(createdJobList.Id);
            jobToUpdate.SetIndex(0);

            await _jobRepository.UpdateAsync(jobToUpdate, cancellationToken);
        }
        
        return new BaseResult<JobListDto, CreateListOutcomes>(
            IsSuccess: true,
            Outcome: CreateListOutcomes.ListCreated,
            Response: new JobListDto()
        );
    }
}
