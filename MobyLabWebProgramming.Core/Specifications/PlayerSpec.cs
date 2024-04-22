using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class PlayerSpec : BaseSpec<PlayerSpec, Player>
{
    public PlayerSpec(Guid id) : base(id)
    {
    }

    public PlayerSpec(string name)
    {
        Query.Where(e => e.Name == name);
    }
}