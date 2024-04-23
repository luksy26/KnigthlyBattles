using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IGameService
{
    public Task<ServiceResponse<GameDTO>> GetGame(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<GameDTO>>> GetGames(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetGameCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddGame(GameAddDTO game, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateGame(GameUpdateDTO game, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteGame(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
