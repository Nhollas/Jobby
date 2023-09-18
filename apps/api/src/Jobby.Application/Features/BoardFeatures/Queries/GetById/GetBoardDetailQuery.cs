using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

public sealed record GetBoardDetailQuery(Guid BoardId) : IRequest<BaseResult<BoardDto, GetBoardDetailOutcomes>>;