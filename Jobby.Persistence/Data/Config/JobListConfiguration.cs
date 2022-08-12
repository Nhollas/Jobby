using Jobby.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class JobListConfiguration : IEntityTypeConfiguration<JobList>
{
    public void Configure(EntityTypeBuilder<JobList> builder)
    {
        builder.HasMany(p => p.Jobs)
            .WithOne(x => x.JobList)
            .HasForeignKey(x => x.JobListFk);
    }
}
