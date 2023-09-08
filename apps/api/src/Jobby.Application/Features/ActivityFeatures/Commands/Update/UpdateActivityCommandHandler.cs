using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update;

internal sealed class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, BaseResult<UpdateActivityResponse, UpdateActivityOutcomes>>
{
    private readonly IRepository<Activity> _activityRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public UpdateActivityCommandHandler(

        IUserService userService,
        IDateTimeProvider timeProvider,
        IRepository<Activity> activityRepository, 
        IRepository<Job> jobRepository, 
        IMapper mapper)
    {
        _userId = userService.UserId();
        _timeProvider = timeProvider;
        _activityRepository = activityRepository;
        _jobRepository = jobRepository;
        _mapper = mapper;
    }

    public async Task<BaseResult<UpdateActivityResponse, UpdateActivityOutcomes>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateActivityCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return new BaseResult<UpdateActivityResponse, UpdateActivityOutcomes>(
                IsSuccess: false,
                Outcome: UpdateActivityOutcomes.ValidationFailure,
                ValidationResult: validationResult
            );
        }
        
        var activityToUpdate = await ResourceProvider<Activity>
            .GetById(_activityRepository.GetByIdAsync)
            .Check(_userId, request.Id, cancellationToken);

        activityToUpdate.Update(
            request.Title,
            request.Type,
            request.StartDate,
            request.EndDate,
            request.Note,
            request.Completed);
        
        if (request.JobId != Guid.Empty && request.JobId != activityToUpdate.JobId)
        {
            var jobToLink = await _jobRepository.GetByIdAsync(request.JobId, cancellationToken);
            
            if (jobToLink == null)
            {
                return new BaseResult<UpdateActivityResponse, UpdateActivityOutcomes>(
                    IsSuccess: false,
                    Outcome: UpdateActivityOutcomes.JobDoesNotExist,
                    ErrorMessage: $"The {nameof(Job)} {request.JobId} you wanted to link doesn't exist."
                );
            }

            if (jobToLink.OwnerId != _userId)
            {
                return new BaseResult<UpdateActivityResponse, UpdateActivityOutcomes>(
                    IsSuccess: false,
                    Outcome: UpdateActivityOutcomes.UnauthorizedJobAccess,
                    ErrorMessage: $"The {nameof(Job)} {request.JobId} you wanted to link doesn't belong to you."
                );
            }

            if (jobToLink.BoardId != activityToUpdate.BoardId)
            {
                return new BaseResult<UpdateActivityResponse, UpdateActivityOutcomes>(
                    IsSuccess: false,
                    Outcome: UpdateActivityOutcomes.JobDoesNotBelongToBoard,
                    ErrorMessage: $"The {nameof(Job)} {request.JobId} you wanted to link doesn't belong to the Board {activityToUpdate.BoardId}."
                );
            }

            activityToUpdate.SetJob(jobToLink);  
        }

        activityToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _activityRepository.UpdateAsync(activityToUpdate, cancellationToken);

        return new BaseResult<UpdateActivityResponse, UpdateActivityOutcomes>(
            IsSuccess: true,
            Outcome: UpdateActivityOutcomes.ActivityUpdated,
            Response: _mapper.Map<UpdateActivityResponse>(activityToUpdate)
        );
    }
}
