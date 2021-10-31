using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface IPathFinder
    {
        List<Vector2> Path { get; set; }
    }
}
