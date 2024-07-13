using AutoMapper;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetList;

internal class GetBoardListQueryHandler(
    IReadRepository<Board> boardRepository,
    IUserService userService,
    IMapper mapper)
    : IRequestHandler<GetBoardListQuery, IDispatchResult<List<BoardDto>>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<List<BoardDto>>> Handle(GetBoardListQuery request, CancellationToken cancellationToken)
    {
        List<Board> boardList = await boardRepository.ListAsync(new GetBoardsFromUserSpecification(_userId), cancellationToken);

        return DispatchResults.Ok(mapper.Map<List<BoardDto>>(boardList));
    }
}
