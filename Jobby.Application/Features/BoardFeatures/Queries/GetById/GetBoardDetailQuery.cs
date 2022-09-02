using Jobby.Application.Contracts.Board;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

public sealed record GetBoardDetailQuery(Guid BoardId) : IRequest<GetBoardResponse>;