using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;

public sealed record JobListDto : EntityDto
{
    public string Name { get; set; }
    public List<PreviewJobDto> Jobs { get; set; }
    public Guid BoardId { get; set; }
}
