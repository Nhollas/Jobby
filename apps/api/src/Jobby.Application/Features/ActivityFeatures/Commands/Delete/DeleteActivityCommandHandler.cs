using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Delete;

internal class DeleteActivityCommandHandler(
    IRepository<Activity> activityRepository,
    IUserService userService)
    : IRequestHandler<DeleteActivityCommand, IDispatchResult<DeleteActivityResponse>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<DeleteActivityResponse>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        Activity? activity = await activityRepository.GetByReferenceAsync(request.ActivityReference, cancellationToken);
        
        if (activity is null)
        {
            return DispatchResults.NotFound<DeleteActivityResponse>(request.ActivityReference);
        }
        
        if (!activity.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<DeleteActivityResponse>($"You are not authorised to access the resource {activity.Reference}.");
        }
        
        await activityRepository.DeleteAsync(activity, cancellationToken);
        
        return DispatchResults.Ok(new DeleteActivityResponse());
    }
}
