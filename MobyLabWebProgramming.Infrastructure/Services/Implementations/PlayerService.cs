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

public class PlayerService : IPlayerService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public PlayerService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponse<PlayerDTO>> GetPlayer(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new PlayerProjectionSpec(id), cancellationToken);
        return result != null ?
             ServiceResponse<PlayerDTO>.ForSuccess(result) :
          ServiceResponse<PlayerDTO>.FromError(CommonErrors.PlayerNotFound);
    }
    public async Task<ServiceResponse<PagedResponse<PlayerDTO>>> GetPlayers(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new PlayerProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<PlayerDTO>>.ForSuccess(result);
    }
    public async Task<ServiceResponse<int>> GetPlayerCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Player>(cancellationToken));

    public async Task<ServiceResponse> AddPlayer(PlayerDTO player, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can add players!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new PlayerSpec(player.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The player already exists!", ErrorCodes.PlayerAlreadyExists));
        }

        await _repository.AddAsync(new Player
        {
            Name = player.Name,
            Age = player.Age,
            Rating = player.Rating,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdatePlayer(PlayerUpdateDTO player, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can update a player", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new PlayerSpec(player.Id), cancellationToken);

        if (entity != null)
        {
            entity.Name = player.Name ?? entity.Name;
            entity.Age = player.Age ?? entity.Age;
            entity.Rating = player.Rating ?? entity.Rating;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeletePlayer(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin &&  requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can delete a player", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Player>(id, cancellationToken);
        return ServiceResponse.ForSuccess();
    }
}
