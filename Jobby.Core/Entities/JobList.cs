using Jobby.Core.Entities.Common;

namespace Jobby.Core.Entities;

public class JobList : BaseEntity
{
    private JobList()
    {
        // required by EF
    }

    public JobList(string listName)
    {
        Name = listName;

        if (Jobs == null)
        {
            Jobs = new List<Job>();
        }
    }

    public string Name { get; set; }
    public ICollection<Job> Jobs { get; set; }

    public Board Board { get; set; }
    public Guid BoardFk { get; set; }
}
