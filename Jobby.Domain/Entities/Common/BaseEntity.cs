namespace Jobby.Domain.Entities.Common;

public abstract class BaseEntity
{
    protected BaseEntity(Guid id, DateTime createdDate, string ownerId)
    {
        Id = id;
        CreatedDate = createdDate;
        OwnerId = ownerId;
    }

    protected BaseEntity()
    {
    }

    public virtual Guid Id { get; protected set; }
    public virtual DateTime CreatedDate { get; protected set; }
    public virtual DateTime LastUpdated { get; protected set; }
    public virtual string OwnerId { get; protected set; }
}
