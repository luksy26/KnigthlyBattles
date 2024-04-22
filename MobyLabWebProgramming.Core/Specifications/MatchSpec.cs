using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class MatchSpec : BaseSpec<MatchSpec, Match>
{
    public MatchSpec(Guid id) : base(id)
    {
    }

    public MatchSpec(string? Tournament_name, string? Player1_name, string? Player2_name, string? TimeControl_Type)
    {
        Query.Where(e => e.Tournament.Name == Tournament_name && e.Player1.Name == Player1_name &&
        e.Player2.Name == Player2_name && e.TimeControl.Type == TimeControl_Type);
    }
}