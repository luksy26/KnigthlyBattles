namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record TimeControlUpdateDTO(Guid Id, string? Type = default, int? TimeInSeconds = default!, int? Increment = default!);
