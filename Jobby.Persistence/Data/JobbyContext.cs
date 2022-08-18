using Jobby.Core.Entities;
using Jobby.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Jobby.Persistence.Data;

public class JobbyContext : DbContext
{
    public JobbyContext(DbContextOptions<JobbyContext> options) : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; }
    public DbSet<JobList> JobLists { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobContact> JobContacts { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastUpdated = DateTime.Now;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
