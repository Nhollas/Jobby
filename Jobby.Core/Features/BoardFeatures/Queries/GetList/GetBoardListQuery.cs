using Jobby.Core.Dtos;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Queries.GetList;

public class GetBoardListQuery : IRequest<List<BoardDto>>
{
}
