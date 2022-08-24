﻿using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(job => job.Id);

        builder.HasMany(x => x.Activities)
            .WithOne(x => x.Job)
            .HasForeignKey(x => x.JobFk);

        builder.HasMany(x => x.Contacts)
            .WithMany(x => x.Jobs)
            .UsingEntity<JobContact>(
                j => j
                    .HasOne(pt => pt.Contact)
                    .WithMany(t => t.JobContacts)
                    .HasForeignKey(pt => pt.ContactFk)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne(pt => pt.Job)
                    .WithMany(p => p.JobContacts)
                    .HasForeignKey(pt => pt.JobFk)
                    .OnDelete(DeleteBehavior.NoAction),
                j =>
                {
                    j.HasKey(t => new { t.JobFk, t.ContactFk });
                });
    }
}
