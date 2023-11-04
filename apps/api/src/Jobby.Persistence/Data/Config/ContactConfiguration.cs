using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(contact => new { contact.Id, contact.Reference });

        builder.OwnsOne(contact => contact.Socials);
        
        builder.HasOne(contact => contact.Board)
            .WithMany(board => board.Contacts)
            .HasForeignKey(contact => new { contact.BoardId, contact.BoardReference })
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(contact => contact.Emails)
            .WithOne(email => email.Contact)
            .HasForeignKey(email => new { email.ContactId, email.ContactReference })
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(contact => contact.Companies)
            .WithOne(company => company.Contact)
            .HasForeignKey(company => new { company.ContactId, company.ContactReference })
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(contact => contact.Phones)
            .WithOne(phone => phone.Contact)
            .HasForeignKey(phone => new { phone.ContactId, phone.ContactReference })
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(contact => contact.Jobs)
            .WithMany(job => job.Contacts)
            .UsingEntity<JobContact>(
                jc => jc
                    .HasOne(pt => pt.Job)
                    .WithMany(t => t.JobContacts)
                    .HasForeignKey(x => new { x.JobId, x.JobReference })
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne(pt => pt.Contact)
                    .WithMany(p => p.JobContacts)
                    .HasForeignKey(x => new { x.ContactId, x.ContactReference })
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey(t => new { t.JobId, t.ContactId });
                });
    }
}
