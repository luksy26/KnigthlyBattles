using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.Property(g => g.Id)
            .IsRequired();
        builder.HasKey(g => g.Id);
        builder.Property(g => g.MatchId)
            .IsRequired();
        builder.Property(g => g.CreatedAt)
            .IsRequired();
        builder.Property(g => g.UpdatedAt)
            .IsRequired();

        builder.HasOne(g => g.Match)
            .WithMany(m => m.Games)
            .HasForeignKey(g => g.MatchId)
            .HasPrincipalKey(m => m.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}