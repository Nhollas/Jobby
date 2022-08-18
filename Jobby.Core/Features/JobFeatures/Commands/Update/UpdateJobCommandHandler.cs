using AutoMapper;
using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.JobFeatures.Commands.Update;
public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Unit>
{
    private readonly IRepository<Job> _repository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public UpdateJobCommandHandler(
        IRepository<Job> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        Job jobToUpdate = await _repository.GetByIdAsync(request.JobId, cancellationToken);

        if (jobToUpdate == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (jobToUpdate.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        _mapper.Map(request, jobToUpdate, typeof(UpdateJobCommand), typeof(Job));

        await _repository.UpdateAsync(jobToUpdate, cancellationToken);

        return Unit.Value;
    }
}
