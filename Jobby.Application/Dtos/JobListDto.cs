namespace Jobby.Application.Dtos;

public sealed record JobListDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Name { get; set; }
    public int Count => Jobs.Count;
    public List<PreviewJobDto> Jobs { get; set; }
}
