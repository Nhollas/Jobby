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

        Count = Jobs.Count;
    }

    public string Name { get; private set; }
    public int Count { get; private set; }
    public ICollection<Job> Jobs { get; set; }


    public Board Board { get; set; }
    public Guid BoardFk { get; set; }


    public void SetCount()
    {
        Count = Jobs.Count;
    }
}
