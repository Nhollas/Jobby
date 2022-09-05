using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public sealed class GetBoardByIdSpec : Specification<Board>
{
    public GetBoardByIdSpec(Guid BoardId)
    {
        Query
            .Where(b => b.Id == BoardId)
            .Include(x => x.JobList)
                .ThenInclude(x => x.Jobs);
    }
}
