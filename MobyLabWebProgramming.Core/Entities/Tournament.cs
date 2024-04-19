namespace MobyLabWebProgramming.Core.Entities;

public class Tournament : BaseEntity
{
    public string Name { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public ICollection<PlayerTournament> PlayerTournaments { get; set; } = default!;
    public ICollection<Match> Matches { get; set; } = default!;
}