using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TimeControlProjectionSpec : BaseSpec<TimeControlProjectionSpec, TimeControl, TimeControlDTO>
{
    protected override Expression<Func<TimeControl, TimeControlDTO>> Spec => e => new()
    {
        Id = e.Id,
        Type = e.Type,
        TimeInSeconds = e.TimeInSeconds,
        Increment = e.Increment,
    };

    public TimeControlProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public TimeControlProjectionSpec(Guid id) : base(id)
    {
    }

    public TimeControlProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Type, searchExpr));
    }
}
