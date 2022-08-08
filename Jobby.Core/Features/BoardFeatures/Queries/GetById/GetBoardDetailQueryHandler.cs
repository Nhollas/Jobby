using AutoMapper;
using Jobby.Core.Dtos;
using Jobby.Core.Entities.BoardAggregate;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Queries.GetById;

public class GetBoardDetailQueryHandler : IRequestHandler<GetBoardDetailQuery, BoardDto>
{
    private readonly IRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetBoardDetailQueryHandler(
        IRepository<Board> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<BoardDto> Handle(GetBoardDetailQuery request, CancellationToken cancellationToken)
    {
        var boardToGet = await _repository.GetByIdAsync(request.BoardId, cancellationToken);

        if (boardToGet == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (boardToGet.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        return _mapper.Map<BoardDto>(boardToGet);
    }
}
