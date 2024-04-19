using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class TimeControlConfiguration : IEntityTypeConfiguration<TimeControl>
{
    public void Configure(EntityTypeBuilder<TimeControl> builder)
    {
        builder.Property(tc => tc.Id)
            .IsRequired();
        builder.HasKey(tc => tc.Id);
        builder.Property(tc => tc.Type)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(tc => tc.TimeInSeconds)
            .IsRequired();
        builder.Property(tc => tc.Increment)
            .IsRequired();
        builder.Property(tc => tc.CreatedAt)
            .IsRequired();
        builder.Property(tc => tc.UpdatedAt)
            .IsRequired();
    }
}
