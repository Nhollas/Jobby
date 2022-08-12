namespace Jobby.Core.Dtos.Common;

public class BaseDto
{
    public virtual Guid Id { get; protected set; }
    public virtual DateTimeOffset CreatedDate { get; protected set; }
    public virtual DateTimeOffset LastUpdated { get; protected set; }
}
