using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class JobListConfiguration : IEntityTypeConfiguration<JobList>
{
    public void Configure(EntityTypeBuilder<JobList> builder)
    {
        builder.HasKey(jobList => new { jobList.Id, jobList.Reference });

        builder.HasMany(jobList => jobList.Jobs)
            .WithOne(job => job.JobList)
            .HasForeignKey(job => new { job.JobListId, job.JobListReference })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
