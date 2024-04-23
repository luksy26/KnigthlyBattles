using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class PlayerProjectionSpec : BaseSpec<PlayerProjectionSpec, Player, PlayerDTO>
{
    protected override Expression<Func<Player, PlayerDTO>> Spec => e => new()
    {
        Id = e.Id,
        Rating = e.Rating,
        Name = e.Name,
        Age = e.Age
    };

    public PlayerProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public PlayerProjectionSpec(Guid id) : base(id)
    {
    }

    public PlayerProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr));
    }
    public PlayerProjectionSpec(string? search, List<Guid> ids)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            Query.Where(e => ids.Contains(e.Id));
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => ids.Contains(e.Id) && EF.Functions.ILike(e.Name, searchExpr));
    }
}
