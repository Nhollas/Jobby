using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;
public sealed class GetContactsFromBoardSpecification : Specification<Contact>
{
    public GetContactsFromBoardSpecification(string boardReference, string userId)
    {
        Query
            .Include(contact => contact.Companies)
            .Include(contact => contact.Phones)
            .Include(contact => contact.Emails)
            .Include(contact => contact.Board)
            .AsNoTracking()
            .Where(contact => contact.BoardReference == boardReference && contact.OwnerId == userId)
            .OrderBy(x => x.CreatedDate);
    }
}
