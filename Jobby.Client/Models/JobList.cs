namespace Jobby.Client.Models;

public class JobList
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    public List<JobPreview> Jobs { get; set; }
}
