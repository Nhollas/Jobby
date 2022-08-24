namespace Jobby.Domain.Entities.Common;

public abstract class BaseEntity
{
    protected BaseEntity(
        DateTime createdDate,
        string ownerId)
    {
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

    public void UpdateEntity(DateTime updatedDate)
    {
        LastUpdated = updatedDate;
    }
}
