namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class PlayerTournamentDTO
{
    public Guid Id { get; set; }
    public PlayerDTO Player { get; set; } = default!;
    public TournamentDTO Tournament { get; set; } = default!;
}
