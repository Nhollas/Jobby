using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class BoardListSpecification : Specification<Board>
{
    public BoardListSpecification(string userId)
    {
        Query
            .Where(board => board.OwnerId == userId);
    }
}
