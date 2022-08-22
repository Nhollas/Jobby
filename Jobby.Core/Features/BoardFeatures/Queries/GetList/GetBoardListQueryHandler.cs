using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Interfaces;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetList;

internal sealed class GetBoardListQueryHandler : IRequestHandler<GetBoardListQuery, List<BoardDto>>
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
