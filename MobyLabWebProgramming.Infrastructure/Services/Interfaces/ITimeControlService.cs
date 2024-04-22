using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITimeControlService
{
    public Task<ServiceResponse<TimeControlDTO>> GetTimeControl(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<TimeControlDTO>>> GetTimeControls(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetTimeControlCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddTimeControl(TimeControlDTO time_control, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateTimeControl(TimeControlUpdateDTO time_control, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteTimeControl(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
