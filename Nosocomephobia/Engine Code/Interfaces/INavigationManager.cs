﻿using Microsoft.Xna.Framework;
using Nosocomephobia.Game_Code.World;
using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.3, 05-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface INavigationManager : IService, IUpdatable
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
    }
}
