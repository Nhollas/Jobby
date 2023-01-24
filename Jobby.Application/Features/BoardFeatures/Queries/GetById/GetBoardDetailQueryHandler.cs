using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

internal sealed class GetBoardDetailQueryHandler : IRequestHandler<GetBoardDetailQuery, GetBoardResponse>
{
    private readonly IReadRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetBoardDetailQueryHandler(
        IReadRepository<Board> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<GetBoardResponse> Handle(GetBoardDetailQuery request, CancellationToken cancellationToken)
    {
        Board board = await ResourceProvider<Board>
            .GetBySpec(_repository.FirstOrDefaultAsync)
            .ApplySpecification(new GetBoardWithRelationshipsSpecification(request.BoardId))
            .Check(_userId);

        return _mapper.Map<GetBoardResponse>(board);

    }
}
