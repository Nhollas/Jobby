using Microsoft.AspNetCore.Identity;

namespace Jobby.Domain.Primitives;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
