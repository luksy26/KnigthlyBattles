namespace MobyLabWebProgramming.Core.Entities;

public class TimeControl : BaseEntity
{
    public string Type { get; set; } = default!; // Time control format (e.g., Rapid, Blitz)
    public int TimeInSeconds { get; set; } = default!;
    public int Increment { get; set; } = default!;
}
