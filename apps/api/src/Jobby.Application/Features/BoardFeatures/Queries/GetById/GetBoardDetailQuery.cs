using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

public sealed record GetBoardDetailQuery(string BoardReference) : IRequest<BaseResult<BoardDto, GetBoardDetailOutcomes>>;