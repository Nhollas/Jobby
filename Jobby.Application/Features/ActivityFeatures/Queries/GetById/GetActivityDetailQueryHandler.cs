using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Queries.GetById;
internal sealed class GetActivityDetailQueryHandler : IRequestHandler<GetActivityDetailQuery, ActivityDto>
{
    private readonly IReadRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetActivityDetailQueryHandler(
        IReadRepository<Board> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<ActivityDto> Handle(GetActivityDetailQuery request, CancellationToken cancellationToken)
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

        var selectedActivity = board.Activities.Where(x => x.Id == request.ActivityId).FirstOrDefault();

        if (selectedActivity is null)
        {
            throw new NotFoundException($"The Board {request.BoardId} does not contain the Activity {request.ActivityId}.");
        }

        return _mapper.Map<ActivityDto>(selectedActivity);
    }
}
