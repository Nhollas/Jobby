using Ardalis.Specification;
using Jobby.Core.Entities.BoardAggregate;

namespace Jobby.Core.Specifications;

public class OwnedBoardsSpecification : Specification<Board>
{
    public OwnedBoardsSpecification(string userId)
    {
        Query.Where(board => board.OwnerId == userId);
    }
}
