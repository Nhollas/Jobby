using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update;
internal sealed class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Unit>
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
            throw new NotFoundException($"The Job {request.JobId} could not be found.");
        }

        if (jobToUpdate.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        _mapper.Map(request, jobToUpdate, typeof(UpdateJobCommand), typeof(Job));

        await _repository.UpdateAsync(jobToUpdate, cancellationToken);

        return Unit.Value;
    }
}
