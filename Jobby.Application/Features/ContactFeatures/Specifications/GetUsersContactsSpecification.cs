using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;

public class GetUsersContactsSpecification : Specification<Contact>
{
    public GetUsersContactsSpecification(string userId)
    {
        Query
            .Include(contact => contact.Companies)
            .Include(contact => contact.Phones)
            .Include(contact => contact.Emails)
            .Include(contact => contact.Jobs)
            .Include(contact => contact.Board)
            .AsNoTracking()
            .Where(contact => contact.OwnerId == userId)
            .OrderBy(x => x.CreatedDate);
    }
}
