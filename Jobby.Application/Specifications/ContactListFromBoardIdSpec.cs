using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class ContactListFromBoardIdSpec : Specification<Contact>
{
    public ContactListFromBoardIdSpec(Guid BoardId, string UserId)
    {
        Query
            .Include(x => x.Jobs)
                .ThenInclude(x => x.Contacts)
            .Include(x => x.Companies)
            .Include(x => x.Phones)
            .Include(x => x.Emails)
            .Where(b => b.BoardId == BoardId && b.OwnerId == UserId);
    }
}
