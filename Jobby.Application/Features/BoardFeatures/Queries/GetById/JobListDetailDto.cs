namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;
public sealed record JobListDetailDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    public List<JobDetailDto> Jobs { get; set; }
}
