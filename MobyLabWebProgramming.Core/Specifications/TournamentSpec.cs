using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class TournamentSpec : BaseSpec<TournamentSpec, Tournament>
{
    public TournamentSpec(Guid id) : base(id)
    {
    }

    public TournamentSpec(string name)
    {
        Query.Where(e => e.Name == name);
    }
}