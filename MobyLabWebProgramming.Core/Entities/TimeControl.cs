namespace MobyLabWebProgramming.Core.Entities;

public class TimeControl : BaseEntity
{
    public string Type { get; set; } = default!; // Time control format (e.g., Rapid, Blitz)
    public int TimeInSeconds { get; set; } = 0;
    public int Increment { get; set; } = 0;
}
