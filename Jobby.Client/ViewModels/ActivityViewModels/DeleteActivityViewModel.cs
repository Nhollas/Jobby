namespace Jobby.Client.ViewModels.ActivityViewModels;

public class DeleteActivityViewModel
{
    public string Title { get; set; }
    public Guid ActivityId { get; set; }
    public Guid BoardId { get; set; }
    public Guid JobId { get; set; }
}
