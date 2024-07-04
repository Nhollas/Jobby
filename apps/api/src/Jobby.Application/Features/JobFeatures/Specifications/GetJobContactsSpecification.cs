using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobFeatures.Specifications;

public  class GetJobContactsSpecification : Specification<Contact>
{
    public GetJobContactsSpecification(string jobReference, string userId)
    {
        Query
            .Include(x => x.Companies)
            .Include(x => x.Phones)
            .Include(x => x.Emails)
            .AsSplitQuery()    
            .AsNoTracking()
            .Where(contact=> contact.Jobs.Any(job => job.Reference == jobReference && job.OwnerId == userId));
    }
}