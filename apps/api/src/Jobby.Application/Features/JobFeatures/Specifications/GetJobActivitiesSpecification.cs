using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobFeatures.Specifications;

public sealed class GetJobActivitiesSpecification : Specification<Activity>
{
    public GetJobActivitiesSpecification(string jobReference, string userId)
    {
        Query
            .Where(activity => activity.JobReference == jobReference && activity.OwnerId == userId)
            .Include(activity => activity.Job)
            .AsNoTracking();
    }
}