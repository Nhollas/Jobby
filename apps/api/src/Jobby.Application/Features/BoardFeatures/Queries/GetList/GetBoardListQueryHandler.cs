using AutoMapper;
using Jobby.Application.Abstractions.Behaviours;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetList;

internal sealed class GetBoardListQueryHandler : IRequestHandler<GetBoardListQuery, List<ListBoardsResponse>>
{
    private readonly IReadRepository<Board> _repository;
    private readonly IMapper _mapper;

    public GetBoardListQueryHandler(
        IReadRepository<Board> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ListBoardsResponse>> Handle(GetBoardListQuery request, CancellationToken cancellationToken)
    {
        var boardSpec = new GetBoardsFromUserSpecification(request.UserId);

        var boardList = await _repository.ListAsync(boardSpec, cancellationToken);

        return _mapper.Map<List<ListBoardsResponse>>(boardList);
    }
}
