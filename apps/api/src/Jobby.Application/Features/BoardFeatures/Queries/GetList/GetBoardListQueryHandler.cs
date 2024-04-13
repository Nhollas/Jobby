using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetList;

internal sealed class GetBoardListQueryHandler : IRequestHandler<GetBoardListQuery, List<BoardDto>>
{
    private readonly IReadRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public GetBoardListQueryHandler(
        IReadRepository<Board> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _userId = userService.UserId();
    }

    public async Task<List<BoardDto>> Handle(GetBoardListQuery request, CancellationToken cancellationToken)
    {
        GetBoardsFromUserSpecification boardSpec = new GetBoardsFromUserSpecification(_userId);

        List<Board> boardList = await _repository.ListAsync(boardSpec, cancellationToken);

        return _mapper.Map<List<BoardDto>>(boardList);
    }
}
