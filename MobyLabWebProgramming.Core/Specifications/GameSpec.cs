using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class GameSpec : BaseSpec<GameSpec, Game>
{
    public GameSpec(Guid id) : base(id)
    {
    }
    public GameSpec(string? Tournament_name, string? Player1_name, string? Player2_name, string? TimeControl_Type, int? GameNumber)
    {
        Query.Where(e => e.Match.Tournament.Name == Tournament_name && e.Match.Player1.Name == Player1_name &&
        e.Match.Player2.Name == Player2_name && e.Match.TimeControl.Type == TimeControl_Type && e.GameNumber == GameNumber);
    }
}