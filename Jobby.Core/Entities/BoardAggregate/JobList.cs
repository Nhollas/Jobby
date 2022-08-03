using Jobby.Core.Entities.Common;

namespace Jobby.Core.Entities.BoardAggregate;

public class JobList : BaseEntity
{
    public string Name { get; private set; }
    public int Count { get; private set; }

    private readonly List<Job> _jobs = new();
    public IReadOnlyCollection<Job> Jobs => _jobs.AsReadOnly();

    private JobList()
    {
        // required by EF
    }

    public JobList(string listName)
    {
        Name = listName;
        Count = _jobs.Count;
    }
}
