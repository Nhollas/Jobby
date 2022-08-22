namespace Jobby.Application.Dtos.Common;

public class BaseDto
{
    public virtual Guid Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime LastUpdated { get; set; }
}
