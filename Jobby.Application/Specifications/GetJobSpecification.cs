using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class GetJobSpecification : Specification<Job>
{

    public GetJobSpecification(Guid JobId)
    {
        Query
            .Where(b => b.Id == JobId)
            .Include(x => x.Contacts)
            .Include(x => x.Activities);

    }
}
