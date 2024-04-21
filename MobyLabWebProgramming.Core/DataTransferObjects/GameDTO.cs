namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class GameDTO
{
    public Guid Id { get; set; }
    public MatchDTO Match { get; set; } = default!;
    public string Result { get; set; } = default!;
}
