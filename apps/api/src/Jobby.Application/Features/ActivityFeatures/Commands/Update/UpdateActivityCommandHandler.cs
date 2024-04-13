using AutoMapper;
using FluentValidation.Results;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update;

internal sealed class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, BaseResult<ActivityDto, UpdateActivityOutcomes>>
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

    public async Task<BaseResult<ActivityDto, UpdateActivityOutcomes>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        UpdateActivityCommandValidator validator = new UpdateActivityCommandValidator();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return new BaseResult<ActivityDto, UpdateActivityOutcomes>(
                IsSuccess: false,
                Outcome: UpdateActivityOutcomes.ValidationFailure,
                ValidationResult: validationResult
            );
        }
        
        ResourceResult<Activity> activityResourceResult = await ResourceProvider<Activity>
            .GetByReference(_activityRepository.GetByReferenceAsync)
            .Check(_userId, request.ActivityReference, cancellationToken);

        if (!activityResourceResult.IsSuccess)
        {
            return new BaseResult<ActivityDto, UpdateActivityOutcomes>(
                IsSuccess: false,
                Outcome: activityResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => UpdateActivityOutcomes.UnauthorizedActivityAccess,
                    Outcome.NotFound => UpdateActivityOutcomes.UnknownActivity,
                    _ => UpdateActivityOutcomes.UnknownError
                },
                ErrorMessage: activityResourceResult.ErrorMessage
            );
        }
        
        Activity activityToUpdate = activityResourceResult.Response;
        
        activityToUpdate.Update(
            request.Title,
            request.Type,
            request.StartDate,
            request.EndDate,
            request.Note,
            request.Completed);
        
        if (request.JobReference != string.Empty && request.JobReference != activityToUpdate.JobReference)
        {
            ResourceResult<Job> jobResourceResult = await ResourceProvider<Job>
                .GetByReference(_jobRepository.GetByReferenceAsync)
                .Check(_userId, request.JobReference, cancellationToken);
            
            if (!jobResourceResult.IsSuccess)
            {
                return new BaseResult<ActivityDto, UpdateActivityOutcomes>(
                    IsSuccess: false,
                    Outcome: jobResourceResult.Outcome switch
                    {
                        Outcome.Unauthorised => UpdateActivityOutcomes.UnauthorizedJobAccess,
                        Outcome.NotFound => UpdateActivityOutcomes.UnknownJob,
                        _ => UpdateActivityOutcomes.UnknownError
                    },
                    ErrorMessage: jobResourceResult.ErrorMessage
                );
            }
            
            Job jobToLink = jobResourceResult.Response;

            if (jobToLink.BoardId != activityToUpdate.BoardId)
            {
                return new BaseResult<ActivityDto, UpdateActivityOutcomes>(
                    IsSuccess: false,
                    Outcome: UpdateActivityOutcomes.JobDoesNotBelongToBoard,
                    ErrorMessage: $"The {nameof(Job)} you wanted to link doesn't have the same Board as the {nameof(Activity)} you provided."
                );
            }

            activityToUpdate.SetJob(jobToLink);  
        }

        activityToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _activityRepository.UpdateAsync(activityToUpdate, cancellationToken);

        return new BaseResult<ActivityDto, UpdateActivityOutcomes>(
            IsSuccess: true,
            Outcome: UpdateActivityOutcomes.ActivityUpdated,
            Response: _mapper.Map<ActivityDto>(activityToUpdate)
        );
    }
}
