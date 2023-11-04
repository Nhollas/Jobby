using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(activity => new { activity.Id, activity.Reference });

        builder.HasOne(activity => activity.Board)
            .WithMany(board => board.Activities)
            .HasForeignKey(activity => new { activity.BoardId, activity.BoardReference })
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(activity => activity.Job)
            .WithMany(job => job.Activities)
            .HasForeignKey(activity => new { activity.JobId, activity.JobReference })
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
