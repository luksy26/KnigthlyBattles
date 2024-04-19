namespace MobyLabWebProgramming.Core.Entities;

public class Match : BaseEntity
{
    public Guid TournamentId { get; set; } = default!;
    public Tournament Tournament { get; set; } = default!;
    public Guid Player1Id { get; set; } = default!;
    public Player Player1 { get; set; } = default!;
    public Guid Player2Id { get; set;} = default!;
    public Player Player2 { get; set; } = default!;
    public Guid TimeControlId { get; set; } = default!;
    public TimeControl TimeControl { get; set; } = default!;
    public ICollection<Game> Games { get; set; } = default!;
    public string Result { get; set; } = default!; // Player1 Won, Player2 Won, Draw

}
