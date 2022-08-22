using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetById;
internal sealed class GetJobDetailQueryHandler : IRequestHandler<GetJobDetailQuery, JobDto>
{
    private readonly IRepository<Job> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetJobDetailQueryHandler(
        IRepository<Job> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<JobDto> Handle(GetJobDetailQuery request, CancellationToken cancellationToken)
    {
        var jobToGet = await _repository.GetByIdAsync(request.JobId, cancellationToken);

        if (jobToGet == null)
        {
            throw new NotFoundException($"The Job {request.JobId} could not be found.");
        }

        if (jobToGet.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        return _mapper.Map<JobDto>(jobToGet);
    }
}
