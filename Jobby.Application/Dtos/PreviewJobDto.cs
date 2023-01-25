using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;
public sealed record PreviewJobDto : EntityDto
{
    public string Company { get; set; }
    public string Title { get; set; }
    public int Index { get; set; }
    public string Colour { get; set; }
    public Guid JobListId { get; set; }
    public Guid BoardId { get; set; }
}
