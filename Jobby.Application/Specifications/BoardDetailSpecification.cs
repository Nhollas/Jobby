using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public sealed class BoardDetailSpecification : Specification<Board>
{
    public BoardDetailSpecification(Guid boardId)
    {
        Query
            .Include(x => x.JobList)
                .ThenInclude(x => x.Jobs)
            .Include(x => x.Contacts)
            .Include(x => x.Activities)
            .AsNoTracking()
            .Where(board => board.Id == boardId);
    }
}
