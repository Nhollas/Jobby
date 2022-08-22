using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasOne(x => x.Board)
            .WithMany(x => x.Activities)
            .HasForeignKey(x => x.BoardFk);

        builder.HasOne(x => x.Job)
            .WithMany(x => x.Activities)
            .HasForeignKey(x => x.JobFk);
    }
}
