namespace MobyLabWebProgramming.Core.Entities;

public class Player : BaseEntity
{
    public string Name { get; set; } = default!;
    public int Rating { get; set; } = default!;
    public int Age { get; set; } = default!;
    public ICollection<PlayerTournament> PlayerTournaments { get; set; } = default!;
}
