using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public sealed class GetBoardWithJobsAndContactsSpec : Specification<Job>
{
    public GetBoardWithJobsAndContactsSpec(Guid BoardId, Guid JobId)
    {
        Query
            .Include(x => x.Contacts)
                .ThenInclude(x => x.Companies)
            .Include(x => x.Contacts)
                .ThenInclude(x => x.Emails)
            .Include(x => x.Contacts)
                .ThenInclude(x => x.Phones)
            .Include(x => x.Activities)
            .Include(x => x.Board)
            .AsNoTracking()
            .Where(b => b.BoardId == BoardId && b.Id == JobId);
    }
}
