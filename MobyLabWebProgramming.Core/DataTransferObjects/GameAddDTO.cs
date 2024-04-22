namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class GameAddDTO
{
    public Guid Id { get; set; }
    public Guid MatchId { get; set; }
    public string Result { get; set; } = default!;
    public int GameNumber { get; set; } = default!;
}
