using Jobby.Client.Models.Common;
using Jobby.Client.Models.JobModels;

namespace Jobby.Client.Models.BoardModels;

public class JobList : BaseEntity
{
    public string Name { get; set; }
    public int Count => Jobs.Count;
    public List<Job> Jobs { get; set; }
}
