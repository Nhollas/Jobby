using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.HasKey(board => new { board.Id, board.Reference });

        builder.HasMany(board => board.Lists)
            .WithOne(jobList => jobList.Board)
            .HasForeignKey(jobList => new { jobList.BoardId, jobList.BoardReference })
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(board => board.Jobs)
            .WithOne(job => job.Board)
            .HasForeignKey(job => new { job.BoardId, job.BoardReference })
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(board => board.Contacts)
            .WithOne(contact => contact.Board)
            .HasForeignKey(contact => new { contact.BoardId, contact.BoardReference })
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(board => board.Activities)
            .WithOne(activity => activity.Board)
            .HasForeignKey(activity => new { activity.BoardId, activity.BoardReference })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
