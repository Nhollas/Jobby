using Jobby.Core.Dtos;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Queries.GetById;

public class GetBoardDetailQuery : IRequest<BoardDto>
{
    public Guid BoardId { get; set; }

    public GetBoardDetailQuery(Guid id)
    {
        BoardId = id;
    }
}
