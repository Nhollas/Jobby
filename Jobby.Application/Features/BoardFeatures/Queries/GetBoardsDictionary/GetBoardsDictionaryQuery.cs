using Jobby.Application.Contracts.Board;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetBoardsDictionary;

public sealed record GetBoardsDictionaryQuery : IRequest<List<BoardDictionaryResponse>>;