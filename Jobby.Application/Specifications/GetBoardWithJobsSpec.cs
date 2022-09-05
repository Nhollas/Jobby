using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;

public class GetBoardWithJobsSpec : Specification<Board>
{
    public GetBoardWithJobsSpec(Guid BoardId)
    {
        Query
            .Where(b => b.Id == BoardId)
            .Include(x => x.JobList)
                .ThenInclude(x => x.Jobs);
    }
}
