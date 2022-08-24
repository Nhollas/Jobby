using Jobby.Application.Dtos;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;
public sealed record BoardDetailDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; }
    public List<JobListDetailDto> JobList { get; set; }
}
