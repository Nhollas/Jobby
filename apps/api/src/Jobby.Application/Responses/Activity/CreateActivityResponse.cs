namespace Jobby.Application.Responses.Activity;

public class CreateActivityResponse
{
    public Guid Id { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime LastUpdated { get; private set; }
    public string OwnerId { get; protected set; }
    public string Title { get; private set; }
    public int Type { get; private set; }
    public string Name { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Note { get; private set; }
    public bool Completed { get; private set; }
}