using Jobby.Client.ViewModels.Common;

namespace Jobby.Client.ViewModels.ActivityViewModels;

public class ActivityDetailViewModel : BaseViewModel
{
    public string Title { get; set; }
    public int ActivityType { get; set; }
    public string ActivityName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }

    public Guid BoardId { get; set; }
    public Guid JobId { get; set; }
}
