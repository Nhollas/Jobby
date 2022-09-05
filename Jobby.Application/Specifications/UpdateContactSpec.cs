using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class UpdateContactSpec : Specification<Contact>
{
    public UpdateContactSpec(Guid contactId)
    {
        Query
            .Include(x => x.Board)
                .ThenInclude(x => x.JobList)
                    .ThenInclude(x => x.Jobs)
             .Include(x => x.JobContacts)
            .Include(x => x.Companies)
            .Include(x => x.Emails)
            .Include(x => x.Phones)
            .Where(contact => contact.Id == contactId);
    }
}
