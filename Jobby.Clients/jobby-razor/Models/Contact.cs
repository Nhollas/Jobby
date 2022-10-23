namespace Jobby.Client.Models;

public class Contact
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public Social Social { get; set; }
    public List<Email> Emails { get; set; }
    public List<Phone> Phones { get; set; }
    public List<Company> Companies { get; set; }
    public List<Job> Jobs { get; set; }
    public BoardPreview Board { get; set; }
}
