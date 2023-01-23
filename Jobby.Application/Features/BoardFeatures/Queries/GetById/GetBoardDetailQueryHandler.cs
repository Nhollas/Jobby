using AutoMapper;
using Jobby.Application.Abstractions.Authorization;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

internal sealed class GetBoardDetailQueryHandler : IRequestHandler<GetBoardDetailQuery, GetBoardResponse>
{
    private readonly IResource<Board> _fetchResource;
    private readonly IRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetBoardDetailQueryHandler(
        IRepository<Board> repository,
        IUserService userService,
        IMapper mapper,
        IResource<Board> fetchResource)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
        _fetchResource = fetchResource;
    }

    public async Task<GetBoardResponse> Handle(GetBoardDetailQuery request, CancellationToken cancellationToken)
    {
        Board board = await _fetchResource
            .WithUser(_userId)
            .TargetResourceId(request.BoardId)
            .FindWith(_repository.FirstOrDefaultAsync)
            .ApplySpecification(new BoardDetailSpecification(request.BoardId))
            .Check();

        return _mapper.Map<GetBoardResponse>(board);

    }
}
