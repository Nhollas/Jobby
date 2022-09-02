using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class JobListConfiguration : IEntityTypeConfiguration<JobList>
{
    public void Configure(EntityTypeBuilder<JobList> builder)
    {
        builder.HasKey(jobList => jobList.Id);

        builder.HasMany(p => p.Jobs)
            .WithOne(x => x.JobList)
            .HasForeignKey(x => x.JobListId);
    }
}
