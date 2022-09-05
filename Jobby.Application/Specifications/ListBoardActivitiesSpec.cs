using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class ListBoardActivitiesSpec : Specification<Activity>
{
    public ListBoardActivitiesSpec(Guid BoardId, string UserId)
    {
        Query
            .Include(x => x.Job)
            .Include(x => x.Board)
            .AsNoTracking()
            .Where(b => b.BoardId == BoardId && b.OwnerId == UserId)
            .OrderBy(x => x.CreatedDate);
    }
}
