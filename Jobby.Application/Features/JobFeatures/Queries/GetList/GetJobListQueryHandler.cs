using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetList;

internal sealed class GetJobListQueryHandler : IRequestHandler<GetJobListQuery, List<PreviewJobDto>>
{
    private readonly IReadRepository<Job> _jobRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;
    
    public GetJobListQueryHandler(
        IUserService userService,
        IMapper mapper,
        IReadRepository<Job> jobRepository)
    {
        _userId = userService.UserId();
        _mapper = mapper;
        _jobRepository = jobRepository;
    }
    
    public async Task<List<PreviewJobDto>> Handle(GetJobListQuery request, CancellationToken cancellationToken)
    {
        var jobs = await _jobRepository.ListAsync(new GetJobsFromUserSpecification(_userId), cancellationToken);
        
        return _mapper.Map<List<PreviewJobDto>>(jobs);
    }
}