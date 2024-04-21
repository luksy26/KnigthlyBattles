namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TournamentDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
}
