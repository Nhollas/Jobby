namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

public record CreateActivityResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string OwnerId { get;  set; }
    public string Title { get; set; }
    public int Type { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
    public Guid? JobId { get; set; }
}