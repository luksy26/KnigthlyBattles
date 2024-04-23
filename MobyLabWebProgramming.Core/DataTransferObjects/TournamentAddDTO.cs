namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TournamentAddDTO
{
    public string Name { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
}
