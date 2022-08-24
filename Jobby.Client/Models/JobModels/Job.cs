using Jobby.Client.Models.ActivityModels;
using Jobby.Client.Models.Common;
using Jobby.Client.Models.ContactModels;

namespace Jobby.Client.Models.JobModels;

public class Job : BaseEntity
{
    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string HexColour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public List<Note> Notes { get; set; }
    public List<Activity> Activities { get; set; }
    public List<Contact> Contacts { get; set; }
}
