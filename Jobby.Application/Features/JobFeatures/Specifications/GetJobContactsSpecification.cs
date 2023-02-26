using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobFeatures.Specifications;

public class GetJobContactsSpecification  : Specification<Contact>
{
    public GetJobContactsSpecification(Guid jobId, string userId)
    {
        Query
            .Include(contact => contact.Companies)
            .Include(contact => contact.Emails)
            .Include(contact => contact.Phones)
            .Include(contact => contact.Socials)
            .AsNoTracking()
            .Where(contact => contact.Jobs.Any(j => j.Id == jobId) && contact.OwnerId == userId);
    }
}