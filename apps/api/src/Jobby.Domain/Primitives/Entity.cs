namespace Jobby.Domain.Primitives;

public abstract class Entity
{
    protected Entity(
        string reference,
        DateTimeOffset createdDate,
        string ownerId)
    {
        Reference = reference;
        CreatedDate = createdDate;
        OwnerId = ownerId;
    }

    protected Entity()
    {
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset CreatedDate { get; protected set; }
    public DateTimeOffset LastUpdated { get; private set; }
    public string OwnerId { get; protected set; }
    
    public string Reference { get; protected set; }

    public void UpdateEntity(DateTimeOffset updatedDate)
    {
        LastUpdated = updatedDate;
    }
    
    public bool IsOwnedBy(string userId)
    {
        return OwnerId == userId;
    }
}
