using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;

public sealed class GetContactWithSocialsSpecification : Specification<Contact>
{
	public GetContactWithSocialsSpecification(Guid contactId)
	{
		Query
			.Include(contact => contact.Socials)
			.AsNoTracking()
			.Where(contact => contact.Id == contactId);
	}
}
