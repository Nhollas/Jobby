using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

public record GetBoardDetailQuery(string BoardReference) : IRequest<IDispatchResult<BoardDto>>;