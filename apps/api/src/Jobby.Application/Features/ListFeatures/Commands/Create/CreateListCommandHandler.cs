using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ListFeatures.Commands.Create;
internal sealed class CreateListCommandHandler : IRequestHandler<CreateListCommand, BaseResult<CreateListResponse, CreateListOutcomes>>
{
    private readonly IRepository<JobList> _jobListRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IGuidProvider _guidProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _userId;

    public CreateListCommandHandler(
        IRepository<JobList> jobListRepository,
        IRepository<Job> jobRepository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider)
    {
        _jobListRepository = jobListRepository;
        _jobRepository = jobRepository;
        _userId = userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
    }

    public async Task<BaseResult<CreateListResponse, CreateListOutcomes>> Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        var createdJobList = JobList.Create(
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Name,
            request.Index,
            request.BoardId);

        await _jobListRepository.AddAsync(createdJobList, cancellationToken);

        if (request.InitJobId != Guid.Empty)
        {
            var jobResourceResult = await ResourceProvider<Job>
                .GetById(_jobRepository.GetByIdAsync)
                .Check(_userId, request.InitJobId, cancellationToken);
            
            if (!jobResourceResult.IsSuccess)
            {
                return new BaseResult<CreateListResponse, CreateListOutcomes>(
                    IsSuccess: false,
                    Outcome: jobResourceResult.Outcome switch
                    {
                        Outcome.Unauthorised => CreateListOutcomes.UnauthorizedJobAccess,
                        Outcome.NotFound => CreateListOutcomes.UnknownJob,
                        _ => CreateListOutcomes.UnknownError
                    },
                    ErrorMessage: jobResourceResult.ErrorMessage
                );
            }
            
            var jobToUpdate = jobResourceResult.Response;

            jobToUpdate.SetJobList(createdJobList.Id);
            jobToUpdate.SetIndex(0);

            await _jobRepository.UpdateAsync(jobToUpdate, cancellationToken);
        }
        
        return new BaseResult<CreateListResponse, CreateListOutcomes>(
            IsSuccess: true,
            Outcome: CreateListOutcomes.ListCreated,
            Response: new CreateListResponse()
        );
    }
}
