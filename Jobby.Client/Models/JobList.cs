using Jobby.Client.Models.Common;

namespace Jobby.Client.Models;

public class JobList : BaseEntity
{
    public string Name { get; set; }
    public int Count => Jobs.Count;
    public ICollection<Job> Jobs { get; set; }
}
