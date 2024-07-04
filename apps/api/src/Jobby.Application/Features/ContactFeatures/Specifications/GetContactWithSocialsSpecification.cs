using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;

public  class GetContactWithSocialsSpecification : Specification<Contact>
{
	public GetContactWithSocialsSpecification(string contactReference)
	{
		Query
			.Include(contact => contact.Socials)
			.Include(contact => contact.Jobs)
			.Where(contact => contact.Reference == contactReference);
	}
}
