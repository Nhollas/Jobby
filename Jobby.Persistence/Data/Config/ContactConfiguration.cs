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

        builder.HasMany(p => p.Emails);
        builder.HasMany(p => p.Companies);
        builder.HasMany(p => p.Phones);

        builder.HasMany(x => x.Jobs)
            .WithMany(x => x.Contacts)
            .UsingEntity<JobContact>(
                j => j
                    .HasOne(pt => pt.Job)
                    .WithMany(t => t.JobContacts)
                    .HasForeignKey(pt => pt.JobFk)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne(pt => pt.Contact)
                    .WithMany(p => p.JobContacts)
                    .HasForeignKey(pt => pt.ContactFk)
                    .OnDelete(DeleteBehavior.NoAction),
                j =>
                {
                    j.HasKey(t => new { t.JobFk, t.ContactFk });
                });
    }
}
