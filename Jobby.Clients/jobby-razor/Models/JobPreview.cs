namespace Jobby.Client.Models;

public class JobPreview
{
    public Guid Id { get; set; }
    public string CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
}
