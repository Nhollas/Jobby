using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;
public sealed class GetBoardsFromUserSpecification : Specification<Board>
{
    public GetBoardsFromUserSpecification(string userId)
    {
        Query
            .AsNoTracking()
            .Include(board => board.Lists)
            .Where(board => board.OwnerId == userId)
            .OrderBy(x => x.CreatedDate);
    }
}
