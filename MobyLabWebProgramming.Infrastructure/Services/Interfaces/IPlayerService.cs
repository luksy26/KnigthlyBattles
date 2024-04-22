using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IPlayerService
{
    public Task<ServiceResponse<PlayerDTO>> GetPlayer(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<PlayerDTO>>> GetPlayers(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetPlayerCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddPlayer(PlayerDTO user, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdatePlayer(PlayerUpdateDTO player, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeletePlayer(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
