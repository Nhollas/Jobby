namespace Jobby.Domain.Entities.Common;

public abstract class BaseEntity
{
    public virtual Guid Id { get; protected set; }
}
