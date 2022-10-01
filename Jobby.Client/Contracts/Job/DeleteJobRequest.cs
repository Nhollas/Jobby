namespace Jobby.Client.Contracts.Job;

public class DeleteJobRequest
{
    public Guid JobId { get; set; }
    public Guid BoardId { get; set; }
    public string JobTitle { get; set; }
    public string JobCompany { get; set; }
}
