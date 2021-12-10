using Microsoft.Xna.Framework;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface IPathFinder
    {
        List<Vector2> Path { get; set; }
    }
}
