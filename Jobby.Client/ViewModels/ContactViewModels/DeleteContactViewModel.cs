namespace Jobby.Client.ViewModels.ContactViewModels;

public class DeleteContactViewModel
{
    public Guid ContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }
    public Guid BoardId { get; set; }
    public Guid JobId { get; set; }
}
