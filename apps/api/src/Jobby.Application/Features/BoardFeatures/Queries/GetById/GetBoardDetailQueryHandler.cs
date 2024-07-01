using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

internal class GetBoardDetailQueryHandler(
    IRepository<Board> boardRepository,
    IUserService userService,
    IMapper mapper)
    : IRequestHandler<GetBoardDetailQuery, IDispatchResult<BoardDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<BoardDto>> Handle(GetBoardDetailQuery request, CancellationToken cancellationToken)
    {
        Board? board = await boardRepository.FirstOrDefaultAsync(new GetBoardWithRelationshipsSpecification(request.BoardReference), cancellationToken);
        
        if (board is null)
        {
            return DispatchResults.NotFound<BoardDto>(request.BoardReference);
        }
        
        if (board.OwnerId != _userId)
        {
            return DispatchResults.Unauthorized<BoardDto>($"You are not authorised to access the resource {board.Reference}.");
        }
        
        return DispatchResults.Ok(mapper.Map<BoardDto>(board));
    }
}
