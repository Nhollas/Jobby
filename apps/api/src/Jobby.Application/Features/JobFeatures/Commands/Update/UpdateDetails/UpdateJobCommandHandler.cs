using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
internal sealed class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, BaseResult<JobDto, UpdateJobOutcomes>>
{
    private readonly IRepository<Job> _jobRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public UpdateJobCommandHandler(
        IRepository<Job> jobRepository,
        IUserService userService,
        IMapper mapper,
        IDateTimeProvider timeProvider)
    {
        _jobRepository = jobRepository;
        _userId = userService.UserId();
        _mapper = mapper;
        _timeProvider = timeProvider;
    }

    public async Task<BaseResult<JobDto, UpdateJobOutcomes>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var jobResourceResult = await ResourceProvider<Job>
            .GetById(_jobRepository.GetByIdAsync)
            .Check(_userId, request.Id, cancellationToken);

        if (!jobResourceResult.IsSuccess)
        {
            return new BaseResult<JobDto, UpdateJobOutcomes>(
                IsSuccess: false,
                Outcome: jobResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => UpdateJobOutcomes.UnauthorizedJobAccess,
                    Outcome.NotFound => UpdateJobOutcomes.UnknownJob,
                    _ => UpdateJobOutcomes.UnknownError
                },
                ErrorMessage: jobResourceResult.ErrorMessage
            );
        }
        
        var jobToUpdate = jobResourceResult.Response;

        _mapper.Map(request, jobToUpdate, typeof(UpdateJobCommand), typeof(Job));

        jobToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _jobRepository.UpdateAsync(jobToUpdate, cancellationToken);
        
        return new BaseResult<JobDto, UpdateJobOutcomes>(
            IsSuccess: true,
            Outcome: UpdateJobOutcomes.JobUpdated,
            Response: new JobDto()
        );
    }
}
