namespace Jobby.Application.Features.ActivityFeatures.Commands.Update;

public record UpdateActivityResponse
{
    public Guid Id { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastUpdated { get; private set; }
    public string OwnerId { get; private  set; }
    public string Title { get; private set; }
    public int Type { get; private set; }
    public string Name { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Note { get; private set; }
    public bool Completed { get; private set; }
};