namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class MatchAddDTO
{
    public Guid Id { get; set; }
    public Guid TournamentId { get; set; }
    public Guid Player1Id { get; set; }
    public Guid Player2Id { get; set; }
    public Guid TimeControlId { get; set; }
    public string Result { get; set; } = default!;
}
