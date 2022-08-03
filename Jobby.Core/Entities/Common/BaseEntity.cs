namespace Jobby.Core.Entities.Common;

public abstract class BaseEntity
{
    public virtual Guid Id { get; protected set; }
}
