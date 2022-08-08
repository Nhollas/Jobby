using Jobby.Core.Entities.Common;

namespace Jobby.Core.Entities.BoardAggregate;

public class JobList : BaseEntity
{
    public string Name { get; private set; }
    public int Count { get; private set; }
    public ICollection<Job> Jobs { get; set; }

    private JobList()
    {
        // required by EF
    }

    public JobList(string listName)
    {
        Name = listName;
        Count = Jobs.Count;
    }
}
