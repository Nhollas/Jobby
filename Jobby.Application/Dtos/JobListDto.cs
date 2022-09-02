namespace Jobby.Application.Dtos;

public sealed record JobListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    public List<PreviewJobDto> Jobs { get; set; }
}
