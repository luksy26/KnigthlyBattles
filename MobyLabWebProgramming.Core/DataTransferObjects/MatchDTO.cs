namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class MatchDTO
{
    public Guid Id { get; set; }
    public TournamentDTO Tournament { get; set; } = default!;
    public PlayerDTO Player1 { get; set; } = default!;
    public PlayerDTO Player2 { get; set;} = default!;
    public TimeControlDTO TimeControl { get; set; } = default!;
    public string Result { get; set; } = default!;
}
