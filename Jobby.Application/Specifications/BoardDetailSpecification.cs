using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public sealed class BoardDetailSpecification : Specification<Board>
{
    public BoardDetailSpecification(Guid boardId)
    {
        Query
            .Include(board => board.JobList
                .OrderBy(list => list.Index)
                )
                .ThenInclude(
                    jobList => jobList.Jobs
                    .OrderBy(job => job.Index)
                )
            .Include(board => board.Contacts)
            .Include(board => board.Activities)
            .AsNoTracking()
            .Where(board => board.Id == boardId);
    }
}
