using Ardalis.Specification;
using Jobby.Core.Entities;

namespace Jobby.Core.Specifications;

public class OwnedBoardsSpecification : Specification<Board>
{
    public OwnedBoardsSpecification(string userId)
    {
        Query
            .Where(board => board.OwnerId == userId)
            .Include(x => x.JobList)
                .ThenInclude(x => x.Jobs)
                    .ThenInclude(x => x.Activities)
            .Include(x => x.Activities)
            .Include(x => x.Contacts);
    }
}