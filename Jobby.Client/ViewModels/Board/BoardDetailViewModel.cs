using Jobby.Client.Models;
using Jobby.Client.ViewModels.Common;

namespace Jobby.Client.ViewModels.Board;

public class BoardDetailViewModel : BaseViewModel
{
    public string Name { get; set; }
    public List<JobList> JobList { get; set; }
    public List<Activity> Activities { get; set; }
    public List<Contact> Contacts { get; set; }
}
