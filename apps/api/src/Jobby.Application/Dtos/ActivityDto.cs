using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;

public sealed record ActivityDto : EntityDto
{
    public string Title { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
    public string BoardReference { get; set; }
    public JobDto Job { get; set; }
}
