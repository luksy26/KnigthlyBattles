namespace MobyLabWebProgramming.Core.Entities;

public class Player : BaseEntity
{
    public string Name { get; set; } = default!;
    public int Rating { get; set; } = 0;
    public int Age { get; set; } = 0;

    public Guid TournamentId { get; set; }
    public Tournament Tournament { get; set; } = default!;
}
