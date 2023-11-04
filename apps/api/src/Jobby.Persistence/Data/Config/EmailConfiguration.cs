using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.HasKey(email => new { email.Id, email.Reference });
        
        builder.HasOne(x => x.Contact)
            .WithMany(x => x.Emails)
            .HasForeignKey(x => new { x.ContactId, x.ContactReference })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
