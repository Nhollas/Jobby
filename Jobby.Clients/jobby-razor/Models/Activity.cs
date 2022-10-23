namespace Jobby.Client.Models;

public class Activity
{
    public Guid Id { get; set; }
    public JobPreview Job { get; set; }
    public BoardPreview Board { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Title { get; set; }
    public string ActivityName { get; set; }
    public int ActivityType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
