using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(company => new { company.Id, company.Reference });

        builder.HasOne(x => x.Contact)
            .WithMany(x => x.Companies)
            .HasForeignKey(x => new { x.ContactId, x.ContactReference })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
