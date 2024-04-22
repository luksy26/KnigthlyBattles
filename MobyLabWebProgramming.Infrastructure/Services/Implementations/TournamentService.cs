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

public class TournamentService : ITournamentService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public TournamentService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponse<TournamentDTO>> GetTournament(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new TournamentProjectionSpec(id), cancellationToken);
        return result != null ?
             ServiceResponse<TournamentDTO>.ForSuccess(result) :
          ServiceResponse<TournamentDTO>.FromError(CommonErrors.TournamentNotFound);
    }
    public async Task<ServiceResponse<PagedResponse<TournamentDTO>>> GetTournaments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new TournamentProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<TournamentDTO>>.ForSuccess(result);
    }
    public async Task<ServiceResponse<int>> GetTournamentCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Tournament>(cancellationToken));

    public async Task<ServiceResponse> AddTournament(TournamentDTO tournament, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin and personnel can add tournaments!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new TournamentSpec(tournament.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The tournament already exists!", ErrorCodes.TournamentAlreadyExists));
        }

        await _repository.AddAsync(new Tournament
        {
            Name = tournament.Name,
            StartDate = tournament.StartDate,
            EndDate = tournament.EndDate,
           
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateTournament(TournamentUpdateDTO tournament, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can add a tournament", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new TournamentSpec(tournament.Id), cancellationToken);

        if (entity != null)
        {
            entity.Name = tournament.Name ?? entity.Name;
            entity.StartDate = tournament.StartDate ?? entity.StartDate;
            entity.EndDate = tournament.EndDate ?? entity.EndDate;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteTournament(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can delete a tournament", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Tournament>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
