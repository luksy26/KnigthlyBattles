using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class TimeControlSpec : BaseSpec<TimeControlSpec, TimeControl>
{
    public TimeControlSpec(Guid id) : base(id)
    {
    }

    public TimeControlSpec(string type)
    {
        Query.Where(e => e.Type == type);
    }
}