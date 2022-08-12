using AutoMapper;
using Jobby.Core.Dtos;
using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using Jobby.Core.Specifications;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Queries.GetList;

public class GetBoardListQueryHandler : IRequestHandler<GetBoardListQuery, List<BoardDto>>
{
    private readonly IRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetBoardListQueryHandler(
        IRepository<Board> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<List<BoardDto>> Handle(GetBoardListQuery request, CancellationToken cancellationToken)
    {
        var boardSpec = new OwnedBoardsSpecification(_userId);

        var boardList = await _repository.ListAsync(boardSpec, cancellationToken);

        if (boardList is null)
        {
            return new List<BoardDto>();
        }

        return _mapper.Map<List<BoardDto>>(boardList);
    }
}
