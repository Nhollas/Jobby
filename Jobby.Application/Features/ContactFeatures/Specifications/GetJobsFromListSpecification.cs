using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;

public sealed class GetJobsFromListSpecification : Specification<Job>
{
    public GetJobsFromListSpecification(ICollection<Guid> jobIds, string userId)
    {
        Query
            .Where(job => job.Id != Guid.Empty && job.OwnerId == userId && jobIds.Contains(job.Id));
    }
}