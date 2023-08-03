using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;
public class GetContactsFromBoardSpecification : Specification<Contact>
{
    public GetContactsFromBoardSpecification(Guid boardId)
    {
        Query
            .Include(contact => contact.Companies)
            .Include(contact => contact.Phones)
            .Include(contact => contact.Emails)
            .Include(contact => contact.Board)
            .AsNoTracking()
            .Where(contact => contact.BoardId == boardId)
            .OrderBy(x => x.CreatedDate);
    }
}
