using Jobby.Client.Models.Common;

namespace Jobby.Client.Models;

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
    public string Notes { get; set; }
    public ICollection<Contact> Contacts { get; set; }
    public ICollection<Activity> Activities { get; set; }
}
