using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.3, 31-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface ICollisionManager : IService, IUpdatable
    {
        /// <summary>
        /// Adds all ICollidables in the Scene Graph to the collidables List on start-up.
        /// </summary>
        void PopulateCollidables(IList<IEntity> sceneGraphCopy);

        /// <summary>
        /// Remove the specified item matching the provided _uName and _uID from the collidables List. Usually called after an item has been
        /// terminated from the game.
        /// </summary>
        /// <param name="uName">The unique name of the object to be removed from collidables.</param>
        /// <param name="uID">The unique ID of the object to be removed from collidables.</param>
        void removeCollidable(String uName, int uID);

        /// <summary>
        /// Add a new Collidable to the collidables List to check it for collisions.
        /// </summary>
        /// <param name="newCollidable">A new ICollidable object to add to the collidables List.</param>
        void addCollidable(ICollidable newCollidable);

        /// <summary>
        /// Iterate through the stored entities and check if a Collision has occured. React appropriately if a collision has occured.
        /// </summary>
        void CheckEntityCollisions();
    }
}
