using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IMatchService
{
    public Task<ServiceResponse<MatchDTO>> GetMatch(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<MatchDTO>>> GetMatches(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetMatchCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddMatch(MatchAddDTO match, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateMatch(MatchUpdateDTO match, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteMatch(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
