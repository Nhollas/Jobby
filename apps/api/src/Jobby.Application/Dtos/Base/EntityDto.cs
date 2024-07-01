namespace Jobby.Application.Dtos.Base;

public record EntityDto
{
    public string Reference { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
}
