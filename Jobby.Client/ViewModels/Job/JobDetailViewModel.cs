using Jobby.Client.Models;
using Jobby.Client.ViewModels.Common;

namespace Jobby.Client.ViewModels.Job;

public class JobDetailViewModel : BaseViewModel
{
    public string Title { get; set; }
    public string Company { get; set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string HexColour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Notes { get; set; }
    public List<Contact> Contacts { get; set; }
    public List<Activity> Activities { get; set; }
}
