using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

public sealed record GetBoardDetailQuery(Guid BoardId) : IRequest<BoardDto>;