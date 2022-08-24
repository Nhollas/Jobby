using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class ContactListFromBoardIdSpec : Specification<Contact>
{
    public ContactListFromBoardIdSpec(Guid BoardId, string UserId)
    {
        Query
            .Where(b => b.BoardFk == BoardId && b.OwnerId == UserId);
    }
}
