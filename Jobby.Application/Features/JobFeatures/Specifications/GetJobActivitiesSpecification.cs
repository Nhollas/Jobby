using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobFeatures.Specifications;

public class GetJobActivitiesSpecification : Specification<Activity>
{
    public GetJobActivitiesSpecification(Guid jobId, string userId)
    {
        Query
            .Where(activity => activity.JobId == jobId && activity.OwnerId == userId)
            .Include(activity => activity.Job)
            .AsNoTracking();
    }
}