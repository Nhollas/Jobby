using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Job;
using Jobby.Application.Features.JobFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetById;
internal sealed class GetJobDetailQueryHandler : IRequestHandler<GetJobDetailQuery, GetJobResponse>
{
    private readonly IReadRepository<Job> _jobRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetJobDetailQueryHandler(
        IUserService userService,
        IMapper mapper,
        IReadRepository<Job> jobRepository)
    {
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<GetJobResponse> Handle(GetJobDetailQuery request, CancellationToken cancellationToken)
    {
        Job job = await ResourceProvider<Job>
            .GetBySpec(_jobRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetJobWithContactsAndActivitiesSpecification(request.BoardId, request.JobId))
            .Check(_userId);

        return _mapper.Map<GetJobResponse>(job);
    }
}
