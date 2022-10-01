using Jobby.Client.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobby.Client.Pages;

public class ContactsModel : PageModel
{
    public Guid BoardId { get; set; }
    public List<ContactList> Contacts { get; set; }

    public void OnGet()
    {
    }
}
