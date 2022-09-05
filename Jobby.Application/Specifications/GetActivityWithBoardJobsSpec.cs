using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public sealed class GetActivityWithBoardJobsSpec : Specification<Activity>
{
	public GetActivityWithBoardJobsSpec(Guid ActivityId)
	{
		Query
			.Where(x => x.Id == ActivityId)
			.Include(x => x.Board.Jobs)
			.AsNoTracking();
	}
}
