using Jobby.Application.Contracts.Board;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetList;

public sealed record GetBoardListQuery : IRequest<List<ListBoardsResponse>>;
