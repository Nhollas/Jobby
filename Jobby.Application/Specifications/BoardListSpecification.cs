using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class BoardListSpecification : Specification<Board>
{
    public BoardListSpecification(string userId)
    {
        Query
            .Include(x => x.Contacts)
            .Include(x => x.Activities)
            .Include(x => x.JobList)
                .ThenInclude(x => x.Jobs)
                    .ThenInclude(x => x.Contacts)
            .Include(x => x.JobList)
                .ThenInclude(x => x.Jobs)
                    .ThenInclude(x => x.Activities)
            .Where(board => board.OwnerId == userId);
    }
}
