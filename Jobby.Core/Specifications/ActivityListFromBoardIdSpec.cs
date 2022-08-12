using Ardalis.Specification;
using Jobby.Core.Entities;

namespace Jobby.Core.Specifications;
public class ActivityListFromBoardIdSpec : Specification<Activity>
{
    public ActivityListFromBoardIdSpec(Guid BoardId, string UserId)
    {
        Query
            .Where(b => b.BoardFk == BoardId && b.OwnerId == UserId);
    }
}
