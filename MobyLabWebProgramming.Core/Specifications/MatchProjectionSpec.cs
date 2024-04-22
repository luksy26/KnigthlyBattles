using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class MatchProjectionSpec : BaseSpec<MatchProjectionSpec, Match, MatchDTO>
{
    protected override Expression<Func<Match, MatchDTO>> Spec => e => new()
    {
        Id = e.Id,
        Tournament = new()
        {   
            Id = e.Tournament.Id,
            Name = e.Tournament.Name,
            StartDate = e.Tournament.StartDate,
            EndDate = e.Tournament.EndDate
        },
        Player1 = new()
        {   
            Id = e.Player1.Id,
            Name = e.Player1.Name,
            Age = e.Player1.Age,
            Rating = e.Player1.Rating
        },
        Player2 = new()
        {   
            Id = e.Player2.Id,
            Name = e.Player2.Name,
            Age = e.Player2.Age,
            Rating = e.Player2.Rating
        },
        TimeControl = new()
        {   
            Id = e.TimeControl.Id,
            Type = e.TimeControl.Type,
            TimeInSeconds = e.TimeControl.TimeInSeconds,
            Increment = e.TimeControl.Increment
        },
        Result = e.Result,
    };

    public MatchProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public MatchProjectionSpec(Guid id) : base(id)
    {
    }

    public MatchProjectionSpec(string? search)
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
