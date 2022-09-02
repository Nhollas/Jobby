namespace Jobby.Domain.Primitives;

public abstract class Entity
{
    protected Entity(
        Guid id,
        DateTime createdDate,
        string ownerId)
    {
        Id = id;
        CreatedDate = createdDate;
        OwnerId = ownerId;
    }

    protected Entity()
    {
    }

    public Guid Id { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime LastUpdated { get; protected set; }
    public string OwnerId { get; protected set; }

    public void UpdateEntity(DateTime updatedDate)
    {
        LastUpdated = updatedDate;
    }
}
