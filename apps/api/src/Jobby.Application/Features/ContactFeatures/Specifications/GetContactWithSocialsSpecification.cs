using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;

public sealed class GetContactWithSocialsSpecification : Specification<Contact>
{
	public GetContactWithSocialsSpecification(Guid contactId)
	{
		Query
			.Include(contact => contact.Socials)
			.Include(contact => contact.Jobs)
			.Where(contact => contact.Id == contactId);
	}
}
