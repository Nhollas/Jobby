using Jobby.Client.Models;

namespace Jobby.Client.ViewModels;

public class ActivityListVM
{
    public Guid BoardId { get; set; }
    public Guid BoardName {get; set;}
    public List<ActivityList> Activities { get; set; }
}
