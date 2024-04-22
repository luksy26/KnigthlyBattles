namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record GameUpdateDTO(Guid Id, MatchUpdateDTO? Match = default, string? Result = default, int? GameNumber = default);
