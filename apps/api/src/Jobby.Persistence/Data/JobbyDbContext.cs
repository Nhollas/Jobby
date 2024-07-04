using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Jobby.Persistence.Data;

public sealed class JobbyDbContext : DbContext
{
    public JobbyDbContext(DbContextOptions<JobbyDbContext> options) : base(options)
    {
    }

    public DbSet<Board> Boards { get; init; }
    public DbSet<JobList> JobLists { get; init; }
    public DbSet<Job> Jobs { get; init; }
    public DbSet<JobContact> JobContacts { get; init; }
    public DbSet<Contact> Contacts { get; init; }
    public DbSet<Activity> Activities { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
