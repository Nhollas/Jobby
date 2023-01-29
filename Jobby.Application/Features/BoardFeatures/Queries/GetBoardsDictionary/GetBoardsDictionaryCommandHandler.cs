using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetBoardsDictionary;

public class GetBoardsDictionaryCommandHandler : IRequestHandler<GetBoardsDictionaryQuery, List<BoardDictionaryResponse>>
{
    private readonly IReadRepository<Board> _repository;
    private readonly string _userId;

    public GetBoardsDictionaryCommandHandler(
        IReadRepository<Board> repository,
        IUserService userService)
    {
        _repository = repository;
        _userId = userService.UserId();
    }
    
    public async Task<List<BoardDictionaryResponse>> Handle(GetBoardsDictionaryQuery request, CancellationToken cancellationToken)
    {
        var boardSpec = new GetBoardsDictionarySpecification(_userId);

        var boardsDictionary = await _repository.ListAsync(boardSpec, cancellationToken);

        return boardsDictionary;
    }
}