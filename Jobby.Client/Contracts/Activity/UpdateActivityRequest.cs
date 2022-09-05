namespace Jobby.Client.Contracts.Activity;

public class UpdateActivityRequest
{
    public Guid ActivityId { get; set; }
    public string Title { get; set; }
    public int ActivityType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
