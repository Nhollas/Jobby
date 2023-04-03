using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobFeatures.Specifications;

public sealed class GetJobContactsSpecification  : Specification<Contact>
{
    public GetJobContactsSpecification(Guid jobId, string userId)
    {
        Query
            .Include(x => x.Companies)
            .Include(x => x.Phones)
            .Include(x => x.Emails)
            .AsSplitQuery()    
            .AsNoTracking()
            .Where(contact=> contact.Jobs.Any(job => job.Id == jobId && job.OwnerId == userId));
    }
}