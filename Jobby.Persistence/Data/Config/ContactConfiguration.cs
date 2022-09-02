using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(contact => contact.Id);

        builder.OwnsOne(p => p.Socials);

        builder.HasMany(p => p.Emails)
            .WithOne(x => x.Contact)
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.Companies)
            .WithOne(x => x.Contact)
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.Phones)
            .WithOne(x => x.Contact)
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(x => x.Jobs)
            .WithMany(x => x.Contacts)
            .UsingEntity<JobContact>(
                j => j
                    .HasOne(pt => pt.Job)
                    .WithMany(t => t.JobContacts)
                    .HasForeignKey(pt => pt.JobId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne(pt => pt.Contact)
                    .WithMany(p => p.JobContacts)
                    .HasForeignKey(pt => pt.ContactId)
                    .OnDelete(DeleteBehavior.NoAction),
                j =>
                {
                    j.HasKey(t => new { t.JobId, t.ContactId });
                });
    }
}
