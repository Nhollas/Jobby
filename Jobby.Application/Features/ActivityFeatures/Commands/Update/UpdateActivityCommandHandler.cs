using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update;

internal sealed class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Unit>
{
    private readonly IRepository<Activity> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public UpdateActivityCommandHandler(
        IRepository<Activity> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        Activity activityToUpdate = await _repository.GetByIdAsync(request.ActivityId, cancellationToken);

        if (activityToUpdate == null)
        {
            throw new NotFoundException($"An activity with id {request.ActivityId} could not be found.");
        }

        if (activityToUpdate.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        _mapper.Map(request, activityToUpdate, typeof(UpdateActivityCommand), typeof(Activity));

        await _repository.UpdateAsync(activityToUpdate, cancellationToken);

        return Unit.Value;
    }
}
