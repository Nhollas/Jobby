using Jobby.Domain.Primitives;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Persistence.Identity;

public class JobbyIdentityContext : IdentityDbContext<ApplicationUser>
{
    public JobbyIdentityContext(DbContextOptions<JobbyIdentityContext> options)
    : base(options)
    {
    }
}
