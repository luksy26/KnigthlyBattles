namespace MobyLabWebProgramming.Core.Entities;

public class Tournament : BaseEntity
{
    public string Name { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;

    public ICollection<Player> Players { get; set; } = default!;
}