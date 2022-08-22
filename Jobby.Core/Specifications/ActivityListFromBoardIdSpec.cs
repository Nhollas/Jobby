using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class ActivityListFromBoardIdSpec : Specification<Activity>
{
    public ActivityListFromBoardIdSpec(Guid BoardId, string UserId)
    {
        Query
            .Where(b => b.BoardFk == BoardId && b.OwnerId == UserId);
    }
}
