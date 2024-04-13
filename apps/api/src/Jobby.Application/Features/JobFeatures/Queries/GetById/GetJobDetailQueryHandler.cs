using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetById;
internal sealed class GetJobDetailQueryHandler : IRequestHandler<GetJobDetailQuery, BaseResult<JobDto, GetJobDetailOutcomes>>
{
    private readonly IReadRepository<Job> _jobRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public GetJobDetailQueryHandler(
        IUserService userService,
        IMapper mapper,
        IReadRepository<Job> jobRepository)
    {
        _userId = userService.UserId();
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<BaseResult<JobDto, GetJobDetailOutcomes>> Handle(GetJobDetailQuery request, CancellationToken cancellationToken)
    {
        ResourceResult<Job> jobResourceResult = await ResourceProvider<Job>
            .GetByReference(_jobRepository.GetByReferenceAsync)
            .Check(_userId, request.JobReference, cancellationToken);

        if (!jobResourceResult.IsSuccess)
        {
            return new BaseResult<JobDto, GetJobDetailOutcomes>(
                IsSuccess: false,
                Outcome: jobResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => GetJobDetailOutcomes.UnauthorizedJobAccess,
                    Outcome.NotFound => GetJobDetailOutcomes.UnknownJob,
                    _ => GetJobDetailOutcomes.UnknownError
                },
                ErrorMessage: jobResourceResult.ErrorMessage
            );
        }
        
        return new BaseResult<JobDto, GetJobDetailOutcomes>(
            IsSuccess: true,
            Outcome: GetJobDetailOutcomes.JobFound,
            Response: _mapper.Map<JobDto>(jobResourceResult.Response)
        );
    }
}
