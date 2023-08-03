using Jobby.Application.Contracts.Board;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetDictionary;

public record GetBoardDictionaryQuery : IRequest<List<BoardDictionaryResponse>>;