using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class PlayerTournamentSpec : BaseSpec<PlayerTournamentSpec, PlayerTournament>
{
    public PlayerTournamentSpec(Guid id) : base(id)
    {
    }

    public PlayerTournamentSpec(string? Tournament_name, string? Player_name)
    {
        Query.Where(e => e.Tournament.Name == Tournament_name && e.Player.Name == Player_name);
    }
    public PlayerTournamentSpec(Guid TournamentId, string filler)
    {
        Query.Where(e => e.TournamentId == TournamentId);
    }
}