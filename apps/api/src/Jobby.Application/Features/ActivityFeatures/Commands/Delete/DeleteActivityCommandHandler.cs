using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Delete;

internal sealed class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, BaseResult<DeleteActivityResponse, DeleteActivityOutcomes>>
{
    private readonly IRepository<Activity> _activityRepository;
    private readonly string _userId;

    public DeleteActivityCommandHandler(
        IRepository<Activity> activityRepository,
        IUserService userService)
    {
        _activityRepository = activityRepository;
        _userId = userService.UserId();
    }

    public async Task<BaseResult<DeleteActivityResponse, DeleteActivityOutcomes>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var activityResourceResult = await ResourceProvider<Activity>
            .GetByReference(_activityRepository.GetByReferenceAsync)
            .Check(_userId, request.ActivityReference, cancellationToken);

        if (!activityResourceResult.IsSuccess)
        {
            return new BaseResult<DeleteActivityResponse, DeleteActivityOutcomes>(
                IsSuccess: false,
                Outcome: activityResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => DeleteActivityOutcomes.UnauthorizedActivityAccess,
                    Outcome.NotFound => DeleteActivityOutcomes.UnknownActivity,
                    _ => DeleteActivityOutcomes.UnknownError
                },
                ErrorMessage: activityResourceResult.ErrorMessage
            );
        }
        
        var activityToDelete = activityResourceResult.Response;

        await _activityRepository.DeleteAsync(activityToDelete, cancellationToken);

        return new BaseResult<DeleteActivityResponse, DeleteActivityOutcomes>(
            IsSuccess: true,
            Outcome: DeleteActivityOutcomes.ActivityDeleted,
            Response: new DeleteActivityResponse()
        );
    }
}
