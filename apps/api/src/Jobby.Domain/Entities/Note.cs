namespace Jobby.Domain.Entities;
public class Note
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public Job Job { get; set; }
    public Guid JobId { get; set; }
}
