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

public class PlayerTournamentService : IPlayerTournamentService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public PlayerTournamentService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponse<PlayerTournamentDTO>> GetPlayerTournament(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new PlayerTournamentProjectionSpec(id), cancellationToken);
        return result != null ?
             ServiceResponse<PlayerTournamentDTO>.ForSuccess(result) :
          ServiceResponse<PlayerTournamentDTO>.FromError(CommonErrors.PlayerTournamentNotFound);
    }
    public async Task<ServiceResponse<PagedResponse<PlayerTournamentDTO>>> GetPlayerTournaments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new PlayerTournamentProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<PlayerTournamentDTO>>.ForSuccess(result);
    }
    public async Task<ServiceResponse<int>> GetPlayerTournamentCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<PlayerTournament>(cancellationToken));

    public async Task<ServiceResponse> AddPlayerTournament(PlayerTournamentAddDTO player_tournament, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin and personnel can add player-tournament entries!", ErrorCodes.CannotAdd));
        }
        var result1 = await _repository.GetAsync(new TournamentProjectionSpec(player_tournament.TournamentId), cancellationToken);
        string? tournament_name = result1?.Name;

        var result2 = await _repository.GetAsync(new PlayerProjectionSpec(player_tournament.PlayerId), cancellationToken);
        string? player_name = result2?.Name;

        var result = await _repository.GetAsync(new PlayerTournamentSpec(tournament_name, player_name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The player-tournament entry already exists!", ErrorCodes.PlayerTournamentAlreadyExists));
        }

        await _repository.AddAsync(new PlayerTournament
        {
            TournamentId = player_tournament.TournamentId,
            PlayerId = player_tournament.PlayerId,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdatePlayerTournament(PlayerTournamentUpdateDTO player_tournament, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can update a player-tournament entry", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new PlayerTournamentSpec(player_tournament.Id), cancellationToken);

        if (entity != null)
        {
            entity.TournamentId = player_tournament.Tournament?.Id ?? entity.TournamentId;
            entity.PlayerId = player_tournament.Player?.Id ?? entity.PlayerId;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeletePlayerTournament(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can delete a player-tournament entry", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<PlayerTournament>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
