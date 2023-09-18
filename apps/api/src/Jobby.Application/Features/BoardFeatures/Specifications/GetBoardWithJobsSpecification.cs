using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;

public class GetBoardWithJobsSpecification : Specification<Board>
{
    public GetBoardWithJobsSpecification(Guid BoardId)
    {
        Query
            .Where(b => b.Id == BoardId)
            .Include(x => x.Lists
                .OrderBy(list => list.Index)
                )
                .ThenInclude(x => x.Jobs
                    .OrderBy(job => job.Index)
                    );
    }
}
