using Jobby.Client.Models.ActivityModels;
using Jobby.Client.Models.ContactModels;
using Jobby.Client.Models.JobModels;
using Jobby.Client.ViewModels.Common;

namespace Jobby.Client.ViewModels.JobViewModels;

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
    public List<Note> Notes { get; set; }
    public List<Contact> Contacts { get; set; }
    public List<Activity> Activities { get; set; }


    // Additional View Model Properties.
    public Guid BoardId { get; set; }
}