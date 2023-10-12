namespace Jobby.Application.Dtos.Base;

public record EntityDto
{
    public string Reference { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
}
