using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;

public class GetUsersContactsSpecification : Specification<Contact>
{
    public GetUsersContactsSpecification(string UserId)
    {
        Query
            .Include(x => x.Companies)
            .Include(x => x.Phones)
            .Include(x => x.Emails)
            .Include(x => x.Jobs)
            .AsNoTracking()
            .Where(contact => contact.OwnerId == UserId)
            .OrderBy(x => x.CreatedDate);
    }
}
