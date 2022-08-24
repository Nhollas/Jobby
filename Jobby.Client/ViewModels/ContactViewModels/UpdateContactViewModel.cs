using Jobby.Client.Models.ContactModels;
using Jobby.Client.Models.JobModels;

namespace Jobby.Client.ViewModels.ContactViewModels;

public class UpdateContactViewModel
{
    public Guid JobId { get; set; }
    public Guid BoardId { get; set; }
    public Guid ContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public Social Socials { get; set; }
    public List<Job> Jobs { get; set; }
    public List<Company> Companies { get; set; }
    public List<Email> Emails { get; set; }
    public List<Phone> Phones { get; set; }
}
