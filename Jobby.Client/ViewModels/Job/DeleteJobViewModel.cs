namespace Jobby.Client.ViewModels.Job;

public class DeleteJobViewModel
{
    public Guid JobId { get; set; }
    public Guid BoardId { get; set; }
    public string JobTitle { get; set; }
    public string JobCompany { get; set; }
}
