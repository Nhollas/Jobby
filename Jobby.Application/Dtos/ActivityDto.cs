namespace Jobby.Application.Dtos;

public sealed record ActivityDto
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public Guid BoardId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Title { get; set; }
    public string ActivityName { get; set; }
    public int ActivityType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
