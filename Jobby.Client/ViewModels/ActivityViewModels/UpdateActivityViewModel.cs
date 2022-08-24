namespace Jobby.Client.ViewModels.ActivityViewModels;

public class UpdateActivityViewModel
{
    public Guid BoardId { get; set; }
    public Guid JobId { get; set; }
    public Guid ActivityId { get; set; }
    public string Title { get; set; }
    public int ActivityType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
