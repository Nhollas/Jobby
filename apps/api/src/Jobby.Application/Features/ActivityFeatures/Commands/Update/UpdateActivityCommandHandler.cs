using AutoMapper;
using FluentValidation.Results;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update;

internal  class UpdateActivityCommandHandler(
    IUserService userService,
    IRepository<Activity> activityRepository,
    IRepository<Job> jobRepository,
    IMapper mapper,
    TimeProvider timeProvider)
    : IRequestHandler<UpdateActivityCommand, IDispatchResult<ActivityDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<ActivityDto>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        UpdateActivityCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return DispatchResults.UnprocessableEntity<ActivityDto>(validationResult);
        }
        
        Activity? activity = await activityRepository.GetByReferenceAsync(request.ActivityReference, cancellationToken);
        
        if (activity is null)
        {
            return DispatchResults.NotFound<ActivityDto>(request.ActivityReference);
        }
        
        if (!activity.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<ActivityDto>(activity.Reference);
        }
        
        activity.Update(
            request.Title,
            request.Type,
            request.StartDate,
            request.EndDate,
            request.Note,
            request.Completed);
        
        if (request.JobReference != string.Empty && request.JobReference != activity.JobReference)
        {
            Job? job = await jobRepository.GetByReferenceAsync(request.JobReference, cancellationToken);
            
            if (job is null)
            {
                return DispatchResults.NotFound<ActivityDto>(request.JobReference);
            }
            
            if (!job.IsOwnedBy(_userId))
            {
                return DispatchResults.Unauthorized<ActivityDto>(job.Reference);
            }
            
            if (!(job.BoardId == activity.BoardId))
            {
                return DispatchResults.BadRequest<ActivityDto>(
                    $"The {nameof(Job)} {request.JobReference} you wanted to link doesn't have the same Board as the {nameof(Activity)} you provided.");
            }

            activity.SetJob(job);  
        }

        activity.UpdateEntity(timeProvider.GetUtcNow());

        await activityRepository.UpdateAsync(activity, cancellationToken);
        
        return DispatchResults.Ok(mapper.Map<ActivityDto>(activity));
    }
}
