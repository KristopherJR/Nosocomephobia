using Microsoft.Xna.Framework;
using Nosocomephobia.Game_Code.World;
using System.Collections.Generic;

namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface INavigationManager
    {
        TileMap NavigationGrid { get; set; } // property
        List<IPathFinder> PathFinders { get; } // read-only property
        /// <summary>
        /// Adds a new IPathFinder to the NavigationManager.
        /// </summary>
        /// <param name="pathFinder">The IPathFinder to add to the NavigationManager.</param>
        void AddPathFinder(IPathFinder pathFinder);
        /// <summary>
        /// Adds a new IPathFinder to the NavigationManager.
        /// </summary>
        /// <param name="pathFinder">The IPathFinder to add to the NavigationManager.</param>
        void RemovePathFinder(IPathFinder pathFinder);
        /// <summary>
        /// Default Update loop.
        /// </summary>
        /// <param name="gameTime">A Snapshot of the GameTime.</param>
        void Update(GameTime gameTime);
    }
}
