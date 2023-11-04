using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(job => new { job.Id, job.Reference });   

        builder.HasMany(x => x.Activities)
            .WithOne(x => x.Job)
            .HasForeignKey(x => x.JobId)
            .HasForeignKey(x => new { x.JobId, x.JobReference })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Contacts)
            .WithMany(x => x.Jobs)
            .UsingEntity<JobContact>(
                j => j
                    .HasOne(pt => pt.Contact)
                    .WithMany(t => t.JobContacts)
                    .HasForeignKey(x => new { x.ContactId, x.ContactReference })
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne(pt => pt.Job)
                    .WithMany(p => p.JobContacts)
                    .HasForeignKey(x => new { x.JobId, x.JobReference })
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey(t => new { t.JobId, t.ContactId });
                });
    }
}
