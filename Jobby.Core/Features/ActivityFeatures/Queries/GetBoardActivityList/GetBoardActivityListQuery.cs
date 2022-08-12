using Jobby.Core.Dtos;
using MediatR;

namespace Jobby.Core.Features.ActivityFeatures.Queries.GetBoardActivityList;
public class GetBoardActivityListQuery : IRequest<List<ActivityDto>>
{
    public Guid BoardId { get; private set; }
    public GetBoardActivityListQuery(Guid id)
    {
        BoardId = id;
    }

}
