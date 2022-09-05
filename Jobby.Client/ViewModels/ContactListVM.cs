using Jobby.Client.Models;

namespace Jobby.Client.ViewModels;

public class ContactListVM
{
    public Guid BoardId { get; set; }
    public List<ContactList> Contacts { get; set; }
}
