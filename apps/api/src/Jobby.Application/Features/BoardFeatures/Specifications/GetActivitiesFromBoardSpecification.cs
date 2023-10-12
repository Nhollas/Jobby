using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;
public class GetActivitiesFromBoardSpecification : Specification<Activity>
{
    public GetActivitiesFromBoardSpecification(string boardReference, string userId)
    {
        Query
            .Include(x => x.Job)
            .AsNoTracking()
            .Where(b => b.BoardReference == boardReference && b.OwnerId == userId)
            .OrderBy(x => x.CreatedDate);
    }
}
