using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;

public sealed class GetBoardWithJobsSpecification : Specification<Board>
{
    public GetBoardWithJobsSpecification(string boardReference)
    {
        Query
            .Where(b => b.Reference == boardReference)
            .Include(x => x.Lists
                .OrderBy(list => list.Index)
                )
                .ThenInclude(x => x.Jobs
                    .OrderBy(job => job.Index)
                    );
    }
}
