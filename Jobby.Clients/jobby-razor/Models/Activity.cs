namespace Jobby.Client.Models;

public class Activity
{
    public Guid Id { get; set; }
    public JobPreview Job { get; set; }
    public BoardPreview Board { get; set; }
    public string CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
