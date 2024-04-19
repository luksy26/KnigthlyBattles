﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.Property(p => p.Id)
            .IsRequired();
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(p => p.Rating)
            .IsRequired();
        builder.Property(p => p.Age)
            .IsRequired();
        builder.Property(p => p.CreatedAt)
            .IsRequired();
        builder.Property(p => p.UpdatedAt)
            .IsRequired();
    }
}