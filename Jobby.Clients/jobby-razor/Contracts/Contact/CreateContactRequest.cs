using Jobby.Client.Models;

namespace Jobby.Client.Contracts.Contact;

public class CreateContactRequest
{
    public Guid BoardId { get; set; }
    public List<Guid> JobIds { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public Social Socials { get; set; }
    public List<Email> Emails { get; set; }
    public List<Phone> Phones { get; set; }
    public List<Company> Companies { get; set; }
}
