using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IPlayerTournamentService
{
    public Task<ServiceResponse<PlayerTournamentDTO>> GetPlayerTournament(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<PlayerTournamentDTO>>> GetPlayerTournaments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetPlayerTournamentCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddPlayerTournament(PlayerTournamentAddDTO player_tournament, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdatePlayerTournament(PlayerTournamentUpdateDTO player_tournament, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeletePlayerTournament(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
