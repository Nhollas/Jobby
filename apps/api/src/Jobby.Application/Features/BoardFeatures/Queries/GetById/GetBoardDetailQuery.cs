using Jobby.Application.Contracts.Board;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

public sealed record GetBoardDetailQuery(Guid BoardId) : IRequest<BaseResult<GetBoardDetailResponse, GetBoardDetailOutcomes>>;