using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobFeatures.Specifications;
public sealed class GetJobWithContactsAndActivitiesSpecification : Specification<Job>
{
    public GetJobWithContactsAndActivitiesSpecification(Guid jobId)
    {
        Query
            .Include(x => x.Contacts)
                .ThenInclude(x => x.Companies)
            .Include(x => x.Contacts)
                .ThenInclude(x => x.Emails)
            .Include(x => x.Contacts)
                .ThenInclude(x => x.Phones)
            .Include(x => x.Activities)
            .AsNoTracking()
            .Where(job => job.Id == jobId);
    }
}
