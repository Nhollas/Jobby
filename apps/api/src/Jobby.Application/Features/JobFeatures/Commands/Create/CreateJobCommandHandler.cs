using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Create;

internal sealed class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, BaseResult<CreateJobResponse, CreateJobOutcomes>>
{
    private readonly IReadRepository<Board> _boardRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public CreateJobCommandHandler(
        IReadRepository<Board> boardRepository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IRepository<Job> jobRepository,
        IGuidProvider guidProvider,
        IMapper mapper)
    {
        _boardRepository = boardRepository;
        _userId = userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _jobRepository = jobRepository;
        _guidProvider = guidProvider;
        _mapper = mapper;
    }

    public async Task<BaseResult<CreateJobResponse, CreateJobOutcomes>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var boardResourceResult = await ResourceProvider<Board>
            .GetBySpec(_boardRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetBoardWithJobsSpecification(request.BoardId))
            .Check(_userId, cancellationToken);
        
        if (!boardResourceResult.IsSuccess)
        {
            return new BaseResult<CreateJobResponse, CreateJobOutcomes>(
                IsSuccess: false,
                Outcome: boardResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => CreateJobOutcomes.UnauthorizedBoardAccess,
                    Outcome.NotFound => CreateJobOutcomes.UnknownBoard,
                    _ => CreateJobOutcomes.UnknownError
                },
                ErrorMessage: boardResourceResult.ErrorMessage
            );
        }
        
        var board = boardResourceResult.Response;

        if (!board.BoardOwnsJoblist(request.JobListId))
        {
            return new BaseResult<CreateJobResponse, CreateJobOutcomes>(
                IsSuccess: false,
                Outcome: CreateJobOutcomes.JobListNotFound,
                ErrorMessage: $"The Board {request.BoardId} does not contain the JobList {request.JobListId}."
            );
        }

        JobList selectedJobList = board.JobLists.First(x => x.Id == request.JobListId);

        int newIndex;

        newIndex = selectedJobList.Jobs.Count == 0 ? 0 : selectedJobList.Jobs.Count;

        var createdJob = Job.Create(
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Company,
            request.Title,
            newIndex,
            selectedJobList,
            board);

        await _jobRepository.AddAsync(createdJob, cancellationToken);

        
        return new BaseResult<CreateJobResponse, CreateJobOutcomes>(
            IsSuccess: true,
            Outcome: CreateJobOutcomes.JobCreated,
            Response: _mapper.Map<CreateJobResponse>(createdJob)
        );        
    }
}
