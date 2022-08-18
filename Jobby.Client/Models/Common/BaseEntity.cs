namespace Jobby.Client.Models.Common;

public abstract class BaseEntity
{
    public virtual Guid Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime LastUpdated { get; set; }
}
