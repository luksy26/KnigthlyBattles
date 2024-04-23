using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class PlayerTournamentProjectionSpec : BaseSpec<PlayerTournamentProjectionSpec, PlayerTournament, PlayerTournamentDTO>
{
    protected override Expression<Func<PlayerTournament, PlayerTournamentDTO>> Spec => e => new()
    {
        Id = e.Id,
        Player = new()
        {
            Id = e.Player.Id,
            Name = e.Player.Name,
            Age = e.Player.Age,
            Rating = e.Player.Rating,
        },
        Tournament = new()
        {
            Id = e.Tournament.Id,
            Name = e.Tournament.Name,
            StartDate = e.Tournament.StartDate,
            EndDate = e.Tournament.EndDate,
        }
    };

    public PlayerTournamentProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public PlayerTournamentProjectionSpec(Guid id) : base(id)
    {
    }

    public PlayerTournamentProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Tournament.Name, searchExpr) || EF.Functions.ILike(e.Player.Name, searchExpr));
    }
}
