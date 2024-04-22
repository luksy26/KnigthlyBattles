using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class GameSpec : BaseSpec<GameSpec, Game>
{
    public GameSpec(Guid id) : base(id)
    {
    }
}