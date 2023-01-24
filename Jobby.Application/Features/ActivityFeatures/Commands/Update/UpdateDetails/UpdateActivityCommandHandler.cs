using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update.UpdateDetails;

internal sealed class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Unit>
{
    private readonly IRepository<Activity> _activityRepository;
    private readonly IUserService _userService;
    private readonly IDateTimeProvider _timeProvider;
    private readonly string _userId;

    public UpdateActivityCommandHandler(

        IUserService userService,
        IDateTimeProvider timeProvider,
        IRepository<Activity> activityRepository)
    {
        _userService = userService;
        _userId = _userService.UserId();
        _timeProvider = timeProvider;
        _activityRepository = activityRepository;
    }

    public async Task<Unit> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        Activity activityToUpdate = await ResourceProvider<Activity>
            .GetById(_activityRepository.GetByIdAsync)
            .Check(_userId, request.Id);

        activityToUpdate.Update(
            request.Title,
            request.Type,
            request.StartDate,
            request.EndDate,
            request.Note,
            request.Completed);

        activityToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _activityRepository.UpdateAsync(activityToUpdate, cancellationToken);

        return Unit.Value;
    }
}
