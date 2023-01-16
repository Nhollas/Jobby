using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public sealed class ListWithJobsSpec : Specification<JobList>
{
	public ListWithJobsSpec(Guid jobListId)
	{
		Query
			.Where(list => list.Id == jobListId)
			.Include(list => list.Jobs);
	}
}
