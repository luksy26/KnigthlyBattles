namespace MobyLabWebProgramming.Core.Entities;

public class PlayerTournament : BaseEntity
{
    public Guid PlayerId { get; set; } = default!;
    public Player Player { get; set; } = default!;
    public Guid TournamentId { get; set; } = default!;
    public Tournament Tournament { get; set; } = default!;
}
