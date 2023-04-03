using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobFeatures.Specifications;
public sealed class GetJobWithContactsAndActivitiesSpecification : Specification<Job>
{
    public GetJobWithContactsAndActivitiesSpecification(Guid jobId)
    {
        Query
            .Include(x => x.Activities)
            .AsNoTracking()
            .Where(job => job.Id == jobId);
    }
}
