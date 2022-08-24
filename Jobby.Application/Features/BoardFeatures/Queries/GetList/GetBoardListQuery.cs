using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetList;

public sealed record GetBoardListQuery : IRequest<List<BoardListDto>>;
