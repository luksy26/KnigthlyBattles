namespace MobyLabWebProgramming.Core.Entities;

public class Game : BaseEntity
{
    public Guid MatchId { get; set; } = default!;
    public Match Match { get; set; } = default!;
    public string Result { get; set; } = default!; // White Won, Black Won, Draw
}
