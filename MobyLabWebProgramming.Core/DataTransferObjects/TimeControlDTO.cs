namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TimeControlDTO
{
    public Guid Id { get; set; }
    public string Type { get; set; } = default!;
    public int TimeInSeconds { get; set; } = default!;
    public int Increment { get; set; } = default!;
}
