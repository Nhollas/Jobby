using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Jobby.Persistence.Data;

public sealed class JobbyDbContext : DbContext
{
    public JobbyDbContext(DbContextOptions<JobbyDbContext> options) : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; }
    public DbSet<JobList> JobLists { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobContact> JobContacts { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
