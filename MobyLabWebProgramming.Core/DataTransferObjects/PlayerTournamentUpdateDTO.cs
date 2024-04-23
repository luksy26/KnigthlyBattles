namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record PlayerTournamentUpdateDTO(Guid Id, PlayerUpdateDTO? Player = default, TournamentUpdateDTO? Tournament = default);
