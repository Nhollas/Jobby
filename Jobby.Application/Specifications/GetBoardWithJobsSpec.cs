using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;

public class GetBoardWithJobsSpec : Specification<Board>
{
    public GetBoardWithJobsSpec(Guid BoardId)
    {
        Query
            .Where(b => b.Id == BoardId)
            .Include(x => x.JobList
                .OrderBy(list => list.Index)
                )
                .ThenInclude(x => x.Jobs
                    .OrderBy(job => job.Index)
                    );
    }
}
