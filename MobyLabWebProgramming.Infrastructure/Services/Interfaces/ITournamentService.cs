using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITournamentService
{
    public Task<ServiceResponse<TournamentDTO>> GetTournament(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<TournamentDTO>>> GetTournaments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<PlayerDTO>>> GetAllPlayers(Guid id, PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetTournamentCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddTournament(TournamentAddDTO tournament, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateTournament(TournamentUpdateDTO player, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteTournament(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
