using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Specifications;
public class BoardWithJobListsSpec : Specification<Board>
{
	public BoardWithJobListsSpec(Guid boardId)
	{
		Query
			.Where(b => b.Id == boardId)
			.Include(l => l.JobList);
	}
}
