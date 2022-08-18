using AutoMapper;
using Jobby.Core.Dtos;
using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.JobFeatures.Queries.GetById;
internal class GetJobDetailQueryHandler : IRequestHandler<GetJobDetailQuery, JobDto>
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
            // TODO: NotFound Problem Details.
        }

        if (jobToGet.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        return _mapper.Map<JobDto>(jobToGet);
    }
}
