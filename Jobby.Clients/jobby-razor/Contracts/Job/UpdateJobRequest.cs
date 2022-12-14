namespace Jobby.Client.Contracts.Job;

public class UpdateJobRequest
{
    public Guid BoardId { get; set; }
    public Guid JobId { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string Colour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Notes { get; set; }
}
