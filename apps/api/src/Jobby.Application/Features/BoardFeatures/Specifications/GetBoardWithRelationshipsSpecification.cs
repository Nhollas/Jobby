using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;

public  class GetBoardWithRelationshipsSpecification : Specification<Board>
{
    public GetBoardWithRelationshipsSpecification(string boardReference)
    {
        Query
            .Include(board => board.Lists
                .OrderBy(list => list.Position)
                )
                .ThenInclude(
                    jobList => jobList.Jobs
                    .OrderBy(job => job.Position)
                )
            .Include(board => board.Contacts)
            .Include(board => board.Activities)
            .AsNoTracking()
            .Where(board => board.Reference == boardReference);
    }
}
