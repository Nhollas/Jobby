namespace Jobby.Core.Entities.Common;

public abstract class BaseEntity
{
    public virtual Guid Id { get; protected set; }
    public virtual DateTime CreatedDate { get; protected set; }
    public virtual DateTime LastUpdated { get; protected set; }
}
