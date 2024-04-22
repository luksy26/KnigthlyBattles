namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record GameUpdateDTO(Guid Id, MatchDTO? Match = default, string? Result = default);
