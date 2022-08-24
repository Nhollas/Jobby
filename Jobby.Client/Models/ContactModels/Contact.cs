using Jobby.Client.Models.Common;
using Jobby.Client.Models.JobModels;

namespace Jobby.Client.Models.ContactModels;

public class Contact : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public Social Socials { get; set; }
    public List<Job> Jobs { get; set; }
    public List<Company> Companies { get; set; }
    public List<Email> Emails { get; set; }
    public List<Phone> Phones { get; set; }
}
