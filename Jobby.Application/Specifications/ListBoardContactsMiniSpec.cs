using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class ListBoardContactsMiniSpec : Specification<Contact>
{
    public ListBoardContactsMiniSpec(Guid BoardId)
    {
        Query
            .Where(b => b.BoardId == BoardId);
    }
}

