using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobFeatures.Specifications;

public class GetJobsFromIdsSpecification : Specification<Job>
{
    public GetJobsFromIdsSpecification(List<Guid> jobIds, string userId)
    {
        Query
            .Where(job => jobIds.Contains(job.Id) && job.OwnerId == userId);
    }
}