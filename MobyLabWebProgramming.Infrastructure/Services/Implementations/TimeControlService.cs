using System.Net;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class TimeControlService : ITimeControlService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public TimeControlService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponse<TimeControlDTO>> GetTimeControl(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new TimeControlProjectionSpec(id), cancellationToken);
        return result != null ?
             ServiceResponse<TimeControlDTO>.ForSuccess(result) :
          ServiceResponse<TimeControlDTO>.FromError(CommonErrors.TimeControlNotFound);
    }
    public async Task<ServiceResponse<PagedResponse<TimeControlDTO>>> GetTimeControls(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new TimeControlProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<TimeControlDTO>>.ForSuccess(result);
    }
    public async Task<ServiceResponse<int>> GetTimeControlCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<TimeControl>(cancellationToken));

    public async Task<ServiceResponse> AddTimeControl(TimeControlDTO time_control, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin and personnel can add time controls!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new TimeControlSpec(time_control.Type), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The time control already exists!", ErrorCodes.TimeControlAlreadyExists));
        }

        await _repository.AddAsync(new TimeControl
        {
            Type = time_control.Type,
            TimeInSeconds = time_control.TimeInSeconds,
            Increment = time_control.Increment

        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateTimeControl(TimeControlUpdateDTO time_control, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can update a time control", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new TimeControlSpec(time_control.Id), cancellationToken);

        if (entity != null)
        {
            entity.Type = time_control.Type ?? entity.Type;
            entity.TimeInSeconds = time_control.TimeInSeconds ?? entity.TimeInSeconds;
            entity.Increment = time_control.Increment ?? entity.Increment;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteTimeControl(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can delete a time control", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<TimeControl>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
