namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record TournamentUpdateDTO(Guid Id, string? Name = default,
    DateTime? StartDate = default, DateTime? EndDate = default);
