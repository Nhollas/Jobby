using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;
public class GetContactWithRelationshipsSpecification : Specification<Contact>
{
    public GetContactWithRelationshipsSpecification(string contactReference)
    {
        Query
            .Include(contact => contact.Jobs)
            .Include(contact => contact.Phones)
            .Include(contact => contact.Emails)
            .Include(contact => contact.Companies)
            .Include(contact => contact.Board)
            .AsNoTracking()
            .Where(contact => contact.Reference == contactReference);
    }
}
