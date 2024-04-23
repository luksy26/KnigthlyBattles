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

public class MatchService : IMatchService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public MatchService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponse<MatchDTO>> GetMatch(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new MatchProjectionSpec(id), cancellationToken);
        return result != null ?
             ServiceResponse<MatchDTO>.ForSuccess(result) :
          ServiceResponse<MatchDTO>.FromError(CommonErrors.MatchNotFound);
    }
    public async Task<ServiceResponse<PagedResponse<MatchDTO>>> GetMatches(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new MatchProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<MatchDTO>>.ForSuccess(result);
    }
    public async Task<ServiceResponse<int>> GetMatchCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Match>(cancellationToken));

    public async Task<ServiceResponse> AddMatch(MatchAddDTO match, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin and personnel can add matches!", ErrorCodes.CannotAdd));
        }
        var result1 = await _repository.GetAsync(new TournamentProjectionSpec(match.TournamentId), cancellationToken);
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
        
        await _repository.AddAsync(new Match
        {
            TournamentId = match.TournamentId,
            Player1Id = match.Player1Id,
            Player2Id = match.Player2Id,
            TimeControlId = match.TimeControlId,
            Result = match.Result
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateMatch(MatchUpdateDTO match, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can update a match", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new MatchSpec(match.Id), cancellationToken);

        if (entity != null)
        {   
            entity.TournamentId = match.Tournament?.Id ?? entity.TournamentId;
            entity.Player1Id = match.Player1?.Id ?? entity.Player1Id;
            entity.Player2Id = match.Player2?.Id ?? entity.Player2Id;
            entity.TimeControlId = match.TimeControl?.Id ?? entity.TimeControlId;
            entity.Result = match.Result ?? entity.Result;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteMatch(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can delete a match", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Match>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
