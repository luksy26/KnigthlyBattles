using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TournamentProjectionSpec : BaseSpec<TournamentProjectionSpec, Tournament, TournamentDTO>
{
    protected override Expression<Func<Tournament, TournamentDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        StartDate = e.StartDate,
        EndDate = e.EndDate,
    };

    public TournamentProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public TournamentProjectionSpec(Guid id) : base(id)
    {
    }

    public TournamentProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr));
    }
}
