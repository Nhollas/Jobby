using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;
public class GetActivitiesFromBoardSpecification : Specification<Activity>
{
    public GetActivitiesFromBoardSpecification(Guid BoardId, string UserId)
    {
        Query
            .Include(x => x.Job)
            .AsNoTracking()
            .Where(b => b.BoardId == BoardId && b.OwnerId == UserId)
            .OrderBy(x => x.CreatedDate);
    }
}
