using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class PhoneConfiguration : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.HasKey(phone => new { phone.Id, phone.Reference });

        builder.HasOne(phone => phone.Contact)
            .WithMany(contact => contact.Phones)
            .HasForeignKey(phone => new { phone.ContactId, phone.ContactReference })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
