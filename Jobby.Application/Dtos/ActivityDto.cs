namespace Jobby.Application.Dtos;

public sealed record ActivityDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
    public Guid BoardId { get; set; }
    public PreviewJobDto Job { get; set; }
}
