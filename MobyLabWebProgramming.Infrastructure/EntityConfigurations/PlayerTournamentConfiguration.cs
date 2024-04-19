using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class PlayerTournamentConfiguration : IEntityTypeConfiguration<PlayerTournament>
{
    public void Configure(EntityTypeBuilder<PlayerTournament> builder)
    {
        builder.Property(pt => pt.TournamentId)
            .IsRequired();
        builder.Property(pt => pt.PlayerId)
            .IsRequired();
        builder.HasKey(pt => pt.Id); // primary key
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();

        builder.HasOne(pt => pt.Player)
            .WithMany(p => p.PlayerTournaments)
            .HasForeignKey(pt => pt.PlayerId)
            .HasPrincipalKey(p => p.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pt => pt.Tournament)
            .WithMany(t => t.PlayerTournaments)
            .HasForeignKey(pt => pt.TournamentId)
            .HasPrincipalKey(t => t.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
