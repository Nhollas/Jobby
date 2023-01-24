using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;
public class GetBoardsFromUserSpecification : Specification<Board>
{
    public GetBoardsFromUserSpecification(string userId)
    {
        Query
            .AsNoTracking()
            .Where(board => board.OwnerId == userId)
            .OrderBy(x => x.CreatedDate);
    }
}
