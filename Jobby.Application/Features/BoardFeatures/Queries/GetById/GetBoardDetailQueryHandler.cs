using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

internal sealed class GetBoardDetailQueryHandler : IRequestHandler<GetBoardDetailQuery, GetBoardResponse>
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

    public async Task<GetBoardResponse> Handle(GetBoardDetailQuery request, CancellationToken cancellationToken)
    {
        var boardSpec = new BoardDetailSpecification(request.BoardId);

        var boardToGet = await _repository.FirstOrDefaultAsync(boardSpec, cancellationToken);

        if (boardToGet == null)
        {
            throw new NotFoundException($"A board with id {request.BoardId} could not be found.");
        }

        if (boardToGet.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        return _mapper.Map<GetBoardResponse>(boardToGet);
    }
}
