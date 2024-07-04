using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;

public sealed class GetBoardWithJobsSpecification : SingleResultSpecification<Board>
{
    public GetBoardWithJobsSpecification(string boardReference)
    {
        Query
            .Where(b => b.Reference == boardReference)
            .Include(x => x.Lists
                .OrderBy(list => list.Position)
                )
                .ThenInclude(x => x.Jobs
                    .OrderBy(job => job.Position)
                    );
    }
}
