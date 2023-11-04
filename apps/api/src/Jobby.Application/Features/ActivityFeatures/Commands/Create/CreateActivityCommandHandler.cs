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
            .WithResource(request.BoardReference)
            .ApplySpecification(new GetBoardWithJobsSpecification(request.BoardReference))
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

        if (request.JobReference != string.Empty)
        {
            if (!boardToLink.BoardOwnsJob(request.JobReference))
            {
                return new BaseResult<ActivityDto, CreateActivityOutcomes>(
                    IsSuccess: false,
                    Outcome: CreateActivityOutcomes.JobDoesNotExistInBoard,
                    ErrorMessage: $"The {nameof(Job)} {request.JobReference} you wanted to link doesn't exist in the Board {request.BoardReference}."
                );
            }

            var jobToLink = boardToLink.Lists
                .SelectMany(x => x.Jobs)
                .First(x => x.Reference == request.JobReference);

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
