using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public sealed class GetContactByIdSpec : Specification<Contact>
{
	public GetContactByIdSpec(Guid ContactId, Guid BoardId)
	{
		Query
			.Include(x => x.Board)
			.Include(x => x.Jobs)
			.Include(x => x.Phones)
			.Include(x => x.Emails)
			.Include(x => x.Companies)
			.AsNoTracking()
			.Where(contact => contact.Id == ContactId && contact.BoardId == BoardId);
    }
}
