namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record MatchUpdateDTO(Guid Id, TournamentUpdateDTO? Tournament = default, PlayerUpdateDTO? Player1 = default,
    PlayerUpdateDTO? Player2 = default, TimeControlUpdateDTO? TimeControl = default, string? Result = default);
