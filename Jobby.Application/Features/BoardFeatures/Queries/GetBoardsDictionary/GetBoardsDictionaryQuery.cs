using Jobby.Application.Contracts.Board;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetBoardsDictionary;

public sealed record GetBoardsDictionaryCommand : IRequest<List<BoardDictionaryResponse>>;