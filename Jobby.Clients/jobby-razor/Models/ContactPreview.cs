namespace Jobby.Client.Models;

public class ContactPreview
{
    public Guid Id { get; set; }
    public BoardPreview Board { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }
    public Social Socials { get; set; }
    public List<Phone> Phones { get; set; }
    public List<Email> Emails { get; set; }
    public List<Company> Companies { get; set; }
}
