namespace Jobby.Core.Entities.Common;

public abstract class BaseEntity
{
    public virtual Guid Id { get; protected set; }
    public virtual DateTimeOffset CreatedDate { get; protected set; }
    public virtual DateTimeOffset LastUpdated { get; protected set; }
}
