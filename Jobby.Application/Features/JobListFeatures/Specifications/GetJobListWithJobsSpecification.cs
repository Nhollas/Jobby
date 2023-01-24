using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobListFeatures.Specifications;
public sealed class GetJobListWithJobsSpecification : Specification<JobList>
{
    public GetJobListWithJobsSpecification(Guid jobListId)
    {
        Query
            .Include(list => list.Jobs)
            .Where(list => list.Id == jobListId);
    }
}
