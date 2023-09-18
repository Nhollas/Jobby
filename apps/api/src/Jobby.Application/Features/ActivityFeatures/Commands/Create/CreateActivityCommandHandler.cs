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

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

internal sealed class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, BaseResult<ActivityDto, CreateActivityOutcomes>>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IRepository<Activity> _activityRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public CreateActivityCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider,
        IRepository<Activity> activityRepository,
        IMapper mapper)
    {
        _boardRepository = repository;
        _userId = userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
        _activityRepository = activityRepository;
        _mapper = mapper;
    }

    public async Task<BaseResult<ActivityDto, CreateActivityOutcomes>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateActivityCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return new BaseResult<ActivityDto, CreateActivityOutcomes>(
                IsSuccess: false,
                Outcome: CreateActivityOutcomes.ValidationFailure,
                ValidationResult: validationResult
            );
        }
        
        var boardResourceResult = await ResourceProvider<Board>
            .GetBySpec(_boardRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetBoardWithJobsSpecification(request.BoardId))
            .Check(_userId, cancellationToken);

        if (!boardResourceResult.IsSuccess)
        {
            return new BaseResult<ActivityDto, CreateActivityOutcomes>(
                IsSuccess: false,
                Outcome: boardResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => CreateActivityOutcomes.UnauthorizedBoardAccess,
                    Outcome.NotFound => CreateActivityOutcomes.UnknownBoard,
                    _ => CreateActivityOutcomes.UnknownError
                },
                ErrorMessage: boardResourceResult.ErrorMessage);
        }
        
        var boardToLink = boardResourceResult.Response;

        var createdActivity = Activity.Create(
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Title,
            (int)request.Type,
            request.StartDate,
            request.EndDate,
            request.Note,
            request.Completed,
            boardToLink);

        if (request.JobId != Guid.Empty)
        {
            if (!boardToLink.BoardOwnsJob(request.JobId))
            {
                return new BaseResult<ActivityDto, CreateActivityOutcomes>(
                    IsSuccess: false,
                    Outcome: CreateActivityOutcomes.JobDoesNotExistInBoard,
                    ErrorMessage: $"The {nameof(Job)} {request.JobId} you wanted to link doesn't exist in the Board {request.BoardId}."
                );
            }

            var jobToLink = boardToLink.Lists
                .SelectMany(x => x.Jobs)
                .First(x => x.Id == request.JobId);

            createdActivity.SetJob(jobToLink);  
        }

        await _activityRepository.AddAsync(createdActivity, cancellationToken);

        return new BaseResult<ActivityDto, CreateActivityOutcomes>(
            IsSuccess: true, 
            Outcome: CreateActivityOutcomes.ActivityCreated,
            Response: _mapper.Map<ActivityDto>(createdActivity)
        );
    }
}
