using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 17-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.World
{
    /// <summary>
    /// Class Door. Represents a physical barrier that the player must 'unlock' to get past.
    /// </summary>
    public class Door : GameEntity, ICollidable
    {
        #region FIELDS
        #endregion

        #region PROPERTIES
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor for class Door.
        /// </summary>
        public Door()
        {
            // SET EntitySprite:
            this.EntitySprite = new Sprite(GameContent.DoorClosed, 0,0,GameContent.DoorClosed.Width, GameContent.DoorClosed.Height);
            // SET the Door as Collidable so the CollisionManager listens for collisions:
            this.isCollidable = true;
        }
        #endregion
    }
}
