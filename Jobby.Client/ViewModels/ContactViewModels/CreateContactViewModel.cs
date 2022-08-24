using Jobby.Client.Models.ContactModels;

namespace Jobby.Client.ViewModels.ContactViewModels;

public class CreateContactViewModel
{
    public Guid BoardId { get; set; }
    public Guid JobId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public List<Guid> JobIds { get; set; }
    public Social Socials { get; set; }
    public List<Email> Emails { get; set; }
    public List<Phone> Phones { get; set; }
    public List<Company> Companies { get; set; }
}
