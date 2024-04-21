namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class PlayerDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Rating { get; set; } = default!;
    public int Age { get; set; } = default!;
}
