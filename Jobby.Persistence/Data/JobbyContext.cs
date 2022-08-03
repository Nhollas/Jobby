﻿using Jobby.Core.Entities.BoardAggregate;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
