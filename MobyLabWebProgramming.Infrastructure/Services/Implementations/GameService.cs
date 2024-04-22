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

public class GameService : IGameService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public GameService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponse<GameDTO>> GetGame(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new GameProjectionSpec(id), cancellationToken);
        return result != null ?
             ServiceResponse<GameDTO>.ForSuccess(result) :
          ServiceResponse<GameDTO>.FromError(CommonErrors.GameNotFound);
    }
    public async Task<ServiceResponse<PagedResponse<GameDTO>>> GetGames(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new GameProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<GameDTO>>.ForSuccess(result);
    }
    public async Task<ServiceResponse<int>> GetGameCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Game>(cancellationToken));

    public async Task<ServiceResponse> AddGame(GameAddDTO game, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin and personnel can add games!", ErrorCodes.CannotAdd));
        }
        /*var result1 = await _repository.GetAsync(new TournamentProjectionSpec(match.TournamentId), cancellationToken);
        string? tournament_name = result1?.Name;

        var result2 = await _repository.GetAsync(new PlayerProjectionSpec(match.Player1Id), cancellationToken);
        string? player1_name = result2?.Name;

        result2 = await _repository.GetAsync(new PlayerProjectionSpec(match.Player2Id), cancellationToken);
        string? player2_name = result2?.Name;

        var result3 = await _repository.GetAsync(new TimeControlProjectionSpec(match.TimeControlId), cancellationToken);
        string? time_control_type = result3?.Type;

        var result = await _repository.GetAsync(new MatchSpec(tournament_name, player1_name, player2_name, time_control_type), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The match already exists!", ErrorCodes.MatchAlreadyExists));
        }
        */
        await _repository.AddAsync(new Game
        {
            MatchId = game.MatchId,
            Result = game.Result,
            GameNumber = game.GameNumber,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateGame(GameUpdateDTO game, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can update a game", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new GameSpec(game.Id), cancellationToken);

        if (entity != null)
        {
            entity.MatchId = game.Match?.Id ?? entity.MatchId;
            entity.Result = game.Result ?? entity.Result;
            entity.GameNumber = game.GameNumber ?? entity.GameNumber;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteGame(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can delete a game", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Game>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
