using Ardalis.Specification;
using Jobby.Core.Entities;

namespace Jobby.Core.Specifications;
public class GetSelectedJobSpecification : Specification<Job>
{
    public GetSelectedJobSpecification(Guid[] JobIds)
    {
        Query
            .Where(j => JobIds.Contains(j.Id));
    }
}
