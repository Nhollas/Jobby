using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;
public class GetBoardWithJobListsSpecification : Specification<Board>
{
    public GetBoardWithJobListsSpecification(Guid boardId)
    {
        Query
            .AsNoTracking()
            .Where(b => b.Id == boardId)
            .Include(l => l.Lists);
    }
}
