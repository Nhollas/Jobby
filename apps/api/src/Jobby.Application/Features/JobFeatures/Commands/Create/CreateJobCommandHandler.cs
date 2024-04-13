using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Create;

internal sealed class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, BaseResult<JobDto, CreateJobOutcomes>>
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

    public async Task<BaseResult<JobDto, CreateJobOutcomes>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        ResourceResult<Board> boardResourceResult = await ResourceProvider<Board>
            .GetBySpec(_boardRepository.FirstOrDefaultAsync)
            .WithResource(request.BoardReference)
            .ApplySpecification(new GetBoardWithJobsSpecification(request.BoardReference))
            .Check(_userId, cancellationToken);
        
        if (!boardResourceResult.IsSuccess)
        {
            return new BaseResult<JobDto, CreateJobOutcomes>(
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
        
        Board board = boardResourceResult.Response;

        if (!board.BoardOwnsList(request.JobListReference))
        {
            return new BaseResult<JobDto, CreateJobOutcomes>(
                IsSuccess: false,
                Outcome: CreateJobOutcomes.JobListNotFound,
                ErrorMessage: $"The Board {request.BoardReference} does not contain the JobList {request.JobListReference}."
            );
        }

        JobList selectedJobList = board.Lists.First(list => list.Reference == request.JobListReference);

        int newIndex;

        newIndex = selectedJobList.Jobs.Count == 0 ? 0 : selectedJobList.Jobs.Count;

        Job createdJob = Job.Create(
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Company,
            request.Title,
            newIndex,
            selectedJobList,
            board);

        await _jobRepository.AddAsync(createdJob, cancellationToken);

        
        return new BaseResult<JobDto, CreateJobOutcomes>(
            IsSuccess: true,
            Outcome: CreateJobOutcomes.JobCreated,
            Response: _mapper.Map<JobDto>(createdJob)
        );        
    }
}
