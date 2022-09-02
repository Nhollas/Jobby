using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class JobContactConfiguration : IEntityTypeConfiguration<JobContact>
{
    public void Configure(EntityTypeBuilder<JobContact> builder)
    {
        builder
            .HasKey(bc => new { bc.ContactId, bc.JobId });

        builder
            .HasOne(bc => bc.Job)
            .WithMany(b => b.JobContacts)
            .HasForeignKey(bc => bc.JobId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(bc => bc.Contact)
            .WithMany(c => c.JobContacts)
            .HasForeignKey(bc => bc.ContactId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
