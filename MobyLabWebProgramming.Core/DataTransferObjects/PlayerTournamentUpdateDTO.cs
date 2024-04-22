namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record PlayerTournamentUpdateDTO(Guid Id, PlayerDTO? Player = default, TournamentDTO? Tournament = default);
