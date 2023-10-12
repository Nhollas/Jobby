using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobFeatures.Specifications;

public class GetJobsFromIdsSpecification : Specification<Job>
{
    public GetJobsFromIdsSpecification(List<string> jobReferences, string userId)
    {
        Query
            .Where(job => jobReferences.Contains(job.Reference) && job.OwnerId == userId);
    }
}