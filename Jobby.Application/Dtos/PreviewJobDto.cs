namespace Jobby.Application.Dtos;
public sealed record PreviewJobDto
{
    public Guid Id { get; set; }
    public string CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
    public int Index { get; set; }
    public Guid JobListId { get; set; }
}
