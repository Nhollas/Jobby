using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.JobFeatures.Specifications;

public sealed class GetJobsFromUserSpecification : Specification<Job>
{
    public GetJobsFromUserSpecification(string userId)
    {
        Query
            .Where(job => job.OwnerId == userId)
            .OrderByDescending(job => job.CreatedDate )
            .AsNoTracking();
    }
}