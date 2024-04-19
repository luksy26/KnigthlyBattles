using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(t => t.StartDate)
            .IsRequired();
        builder.Property(t => t.EndDate)
            .IsRequired();
        builder.Property(t => t.CreatedAt)
            .IsRequired();
        builder.Property(t => t.UpdatedAt)
            .IsRequired();
    }
}