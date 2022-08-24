using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class BoardDetailSpecification : Specification<Board>
{
    public BoardDetailSpecification(Guid boardId)
    {
        Query
            .Where(board => board.Id == boardId)
                .Include(x => x.JobList)
                    .ThenInclude(x => x.Jobs);
    }
}
