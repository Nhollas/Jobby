using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;

public sealed class GetBoardWithRelationshipsSpecification : Specification<Board>
{
    public GetBoardWithRelationshipsSpecification(Guid boardId)
    {
        Query
            .Include(board => board.Lists
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
