using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.HasKey(company => company.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasOne(x => x.Contact)
            .WithMany(x => x.Emails)
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
