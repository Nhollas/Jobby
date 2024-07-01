using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetList;

public record GetBoardListQuery : IRequest<IDispatchResult<List<BoardDto>>>;
