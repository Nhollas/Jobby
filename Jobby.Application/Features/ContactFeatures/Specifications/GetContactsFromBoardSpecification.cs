using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;
public class GetContactsFromBoardSpecification : Specification<Contact>
{
    public GetContactsFromBoardSpecification(Guid BoardId, string UserId)
    {
        Query
            .Include(contact => contact.Companies)
            .Include(contact => contact.Phones)
            .Include(contact => contact.Emails)
            .Include(contact => contact.Board)
            .AsNoTracking()
            .Where(contact => contact.BoardId == BoardId && contact.OwnerId == UserId)
            .OrderBy(x => x.CreatedDate);
    }
}
