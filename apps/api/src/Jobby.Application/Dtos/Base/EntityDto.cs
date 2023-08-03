namespace Jobby.Application.Dtos.Base;

public record EntityDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
}
