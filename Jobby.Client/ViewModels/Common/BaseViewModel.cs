namespace Jobby.Client.ViewModels.Common;

public abstract class BaseViewModel
{
    public virtual Guid Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual string FormatedCreatedDate => CreatedDate.ToLongDateString();
    public virtual DateTime LastUpdated { get; set; }
}
