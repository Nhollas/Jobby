using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.ContactFeatures.Specifications;

public  class GetJobsFromListSpecification : Specification<Job>
{
    public GetJobsFromListSpecification(ICollection<string> jobReferences, string userId)
    {
        Query
            .Where(job => job.Id != Guid.Empty && job.OwnerId == userId && jobReferences.Contains(job.Reference));
    }
}