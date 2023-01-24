using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
internal sealed class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Unit>
{
    private readonly IRepository<Job> _jobRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public UpdateJobCommandHandler(
        IRepository<Job> jobRepository,
        IUserService userService,
        IMapper mapper,
        IDateTimeProvider timeProvider)
    {
        _jobRepository = jobRepository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        Job jobToUpdate = await ResourceProvider<Job>
            .GetById(_jobRepository.GetByIdAsync)
            .Check(_userId, request.JobId);

        _mapper.Map(request, jobToUpdate, typeof(UpdateJobCommand), typeof(Job));

        jobToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _jobRepository.UpdateAsync(jobToUpdate, cancellationToken);

        return Unit.Value;
    }
}
