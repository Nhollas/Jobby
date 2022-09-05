using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class ListBoardContactsSpec : Specification<Contact>
{
    public ListBoardContactsSpec(Guid BoardId, string UserId)
    {
        Query
            .Include(x => x.Companies)
            .Include(x => x.Phones)
            .Include(x => x.Emails)
            .Include(x => x.Board)
            .AsNoTracking()
            .Where(b => b.BoardId == BoardId && b.OwnerId == UserId)
            .OrderBy(x => x.CreatedDate);
    }
}
