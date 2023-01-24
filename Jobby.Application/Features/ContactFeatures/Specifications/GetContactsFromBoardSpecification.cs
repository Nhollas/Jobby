using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;
public class GetContactsFromBoardSpecification : Specification<Contact>
{
    public GetContactsFromBoardSpecification(Guid BoardId, string UserId)
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
