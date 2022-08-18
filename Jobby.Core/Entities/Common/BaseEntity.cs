namespace Jobby.Core.Entities.Common;

public abstract class BaseEntity
{
    public virtual Guid Id { get; init; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime LastUpdated { get; set; }
    public virtual string OwnerId { get; init; }
}
