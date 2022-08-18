using Jobby.Client.Models.Common;

namespace Jobby.Client.Models;

public class Contact : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public Social Social { get; set; }
    public ICollection<string> Companies { get; set; }
    public ICollection<string> Emails { get; set; }
    public ICollection<string> Phones { get; set; }
}
