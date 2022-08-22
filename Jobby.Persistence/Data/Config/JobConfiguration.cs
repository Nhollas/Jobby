using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasMany(x => x.Activities)
            .WithOne(x => x.Job)
            .HasForeignKey(x => x.JobFk);
    }
}
