using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.Property(m => m.Id)
            .IsRequired();
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Player1Id)
            .IsRequired();
        builder.Property(m => m.Player2Id)
            .IsRequired();
        builder.Property(m => m.TournamentId)
            .IsRequired();
        builder.Property(m => m.TimeControlId)
            .IsRequired();
        builder.Property(m => m.CreatedAt)
            .IsRequired();
        builder.Property(m => m.UpdatedAt)
            .IsRequired();

        builder.HasOne(m => m.TimeControl)
            .WithMany()
            .HasForeignKey(m => m.TimeControlId)
            .HasPrincipalKey(tc => tc.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Tournament)
            .WithMany(t => t.Matches)
            .HasForeignKey(m => m.TournamentId)
            .HasPrincipalKey(t => t.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Player1)
            .WithMany()
            .HasForeignKey(m => m.Player1Id)
            .HasPrincipalKey(p => p.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Player2)
            .WithMany()
            .HasForeignKey(m => m.Player2Id)
            .HasPrincipalKey(p => p.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}