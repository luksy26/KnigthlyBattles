using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class GameProjectionSpec : BaseSpec<GameProjectionSpec, Game, GameDTO>
{
    protected override Expression<Func<Game, GameDTO>> Spec => e => new()
    {
        Id = e.Id,
        Match = new()
        {
            Id = e.Match.Id,
            Tournament = new()
            {
                Id = e.Match.Tournament.Id,
                Name = e.Match.Tournament.Name,
                StartDate = e.Match.Tournament.StartDate,
                EndDate = e.Match.Tournament.EndDate
            },
            Player1 = new()
            {   
                Id = e.Match.Player1.Id,
                Name = e.Match.Player1.Name,
                Age = e.Match.Player1.Age,
                Rating = e.Match.Player1.Rating
            },
            Player2 = new()
            {
                Id = e.Match.Player2.Id,
                Name = e.Match.Player2.Name,
                Age = e.Match.Player2.Age,
                Rating = e.Match.Player2.Rating
            },
            TimeControl = new()
            {
                Id = e.Match.TimeControl.Id,
                Type = e.Match.TimeControl.Type,
                TimeInSeconds = e.Match.TimeControl.TimeInSeconds,
                Increment = e.Match.TimeControl.Increment
            }
        },
        Result = e.Result,
        GameNumber = e.GameNumber,
    };

    public GameProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public GameProjectionSpec(Guid id) : base(id)
    {
    }
    public GameProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Result, searchExpr));
    }
}
