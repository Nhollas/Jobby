using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class ListOwnedBoardsSpec : Specification<Board>
{
    public ListOwnedBoardsSpec(string userId)
    {
        Query
            .AsNoTracking()
            .Where(board => board.OwnerId == userId)
            .OrderBy(x => x.CreatedDate);
    }
}
