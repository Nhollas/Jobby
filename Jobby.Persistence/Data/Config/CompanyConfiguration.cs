using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(company => company.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasOne(x => x.Contact)
            .WithMany(x => x.Companies)
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
