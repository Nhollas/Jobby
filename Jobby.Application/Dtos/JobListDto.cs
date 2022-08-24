namespace Jobby.Application.Dtos;

public sealed record JobListDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    public List<JobDto> Jobs { get; set; }
}
