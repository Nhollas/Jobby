using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Job;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetById;
internal sealed class GetJobDetailQueryHandler : IRequestHandler<GetJobDetailQuery, GetJobResponse>
{
    private readonly IReadRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetJobDetailQueryHandler(
        IReadRepository<Board> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<GetJobResponse> Handle(GetJobDetailQuery request, CancellationToken cancellationToken)
    {
        var boardSpec = new GetBoardByIdSpec(request.BoardId);

        var board = await _repository.FirstOrDefaultAsync(boardSpec, cancellationToken);

        if (board is null)
        {
            throw new NotFoundException($"The Board {request.BoardId} could not be found.");
        }

        if (board.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        var selectedJob = board.JobList.SelectMany(x => x.Jobs).Where(x => x.Id == request.JobId).FirstOrDefault();

        if (selectedJob is null)
        {
            throw new NotFoundException($"The Board {request.BoardId} does not contain the Job {request.JobId}.");
        }

        return _mapper.Map<GetJobResponse>(selectedJob);
    }
}
