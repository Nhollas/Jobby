﻿using Jobby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.Data.Config;
public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.HasMany(x => x.JobList)
            .WithOne(x => x.Board)
            .HasForeignKey(x => x.BoardFk);

        builder.HasMany(x => x.Contacts)
            .WithOne(x => x.Board)
            .HasForeignKey(x => x.BoardFk);

        builder.HasMany(x => x.Activities)
            .WithOne(x => x.Board)
            .HasForeignKey(x => x.BoardFk);
    }
}