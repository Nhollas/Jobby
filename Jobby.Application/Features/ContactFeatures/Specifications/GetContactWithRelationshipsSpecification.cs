using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;
public sealed class GetContactWithRelationshipsSpecification : Specification<Contact>
{
    public GetContactWithRelationshipsSpecification(Guid ContactId, Guid BoardId)
    {
        Query
            .Include(x => x.Jobs)
            .Include(x => x.Phones)
            .Include(x => x.Emails)
            .Include(x => x.Companies)
            .AsNoTracking()
            .Where(contact => contact.Id == ContactId && contact.BoardId == BoardId);
    }
}
