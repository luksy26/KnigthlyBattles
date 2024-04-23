namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TimeControlAddDTO
{
    public string Type { get; set; } = default!;
    public int TimeInSeconds { get; set; } = default!;
    public int Increment { get; set; } = default!;
}
