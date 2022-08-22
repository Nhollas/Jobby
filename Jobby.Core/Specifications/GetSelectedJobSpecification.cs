using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class GetSelectedJobSpecification : Specification<Job>
{
    public GetSelectedJobSpecification(Guid[] JobIds)
    {
        Query
            .Where(j => JobIds.Contains(j.Id));
    }
}
