using Jobby.Client.Models.Common;

namespace Jobby.Client.Models;

public class Board : BaseEntity
{
    public string Name { get; set; }
    public List<JobList> JobList { get; set; }
    public List<Activity> Activities { get; set; }
    public List<Contact> Contacts { get; set; }
}
