using Jobby.Client.Models;

namespace Jobby.Client.ViewModels;

public class ViewBoardVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<JobList> JobList { get; set; }
    public int ActivitiesCount { get; set; }
    public int ContactsCount { get; set; }
}
