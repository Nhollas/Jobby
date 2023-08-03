using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetDictionary;

internal sealed class GetBoardDictionaryQueryHandler : IRequestHandler<GetBoardDictionaryQuery, List<BoardDictionaryResponse>>
{
    private readonly IReadRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public GetBoardDictionaryQueryHandler(
        IReadRepository<Board> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userId = userService.UserId();
        _mapper = mapper;
    }
    public async Task<List<BoardDictionaryResponse>> Handle(GetBoardDictionaryQuery request, CancellationToken cancellationToken)
    {
        var boards = await _repository.ListAsync(new GetBoardsDictionarySpecification(_userId), cancellationToken);
        
        return _mapper.Map<List<BoardDictionaryResponse>>(boards);
    }
}