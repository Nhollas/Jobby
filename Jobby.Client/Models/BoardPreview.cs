namespace Jobby.Client.Models;

public class BoardPreview
{
    public virtual Guid Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime LastUpdated { get; set; }
    public string Name { get; set; }
}
