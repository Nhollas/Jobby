using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class JobContactConfiguration : IEntityTypeConfiguration<JobContact>
{
    public void Configure(EntityTypeBuilder<JobContact> builder)
    {
        builder.HasKey(jc => new { jc.ContactId, jc.JobId });

        builder
            .HasOne(jc => jc.Job)
            .WithMany(job => job.JobContacts)
            .HasForeignKey(jc => new { jc.JobId, jc.JobReference })
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(jc => jc.Contact)
            .WithMany(contact => contact.JobContacts)
            .HasForeignKey(jc => new { jc.ContactId, jc.ContactReference })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
