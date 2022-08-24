using Jobby.Client.Models.ActivityModels;
using Jobby.Client.Models.Common;
using Jobby.Client.Models.ContactModels;

namespace Jobby.Client.Models.BoardModels;

public class Board : BaseEntity
{
    public string Name { get; set; }
    public List<JobList> JobList { get; set; }
    public List<Activity> Activities { get; set; }
    public List<Contact> Contacts { get; set; }
}
