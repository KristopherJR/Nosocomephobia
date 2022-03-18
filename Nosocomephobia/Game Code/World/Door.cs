using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 18-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.World
{
    /// <summary>
    /// Class Door. Represents a physical barrier that the player must 'unlock' to get past.
    /// </summary>
    public class Door : GameEntity, ICollidable
    {
        #region FIELDS
        // DECLARE a bool, call it _isLocked:
        private bool _isLocked;
        #endregion

        #region PROPERTIES
        // DECLARE a get property for IsLocked:
        public bool IsLocked
        {
            get { return _isLocked; }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor for class Door.
        /// </summary>
        public Door()
        {
            // SET EntitySprite:
            this.EntitySprite = new Sprite(GameContent.DoorClosed, 0, 0, GameContent.DoorClosed.Width, GameContent.DoorClosed.Height);
            // SET the Door as Collidable so the CollisionManager listens for collisions:
            this.isCollidable = true;
            // SET the door to locked by default:
            _isLocked = true;
        }

        /// <summary>
        /// Unlocks the door.
        /// </summary>
        public void Unlock()
        {
            // TURN off the doors collider:
            this.IsCollidable = false;
            // CHANGE the doors Sprite to unlocked:
            this.EntitySprite = new Sprite(GameContent.DoorOpen, 0, 0, GameContent.DoorOpen.Width, GameContent.DoorOpen.Height);
            // PLAY the unlock sfx:
            GameContent.DoorUnlock.Play(0.3f,0.0f,0.0f);
            // FLAG that the door is no longer locked:
            _isLocked = false;
        }

        /// <summary>
        /// Locks the door.
        /// </summary>
        public void Lock()
        {
            // TURN on the doors collider:
            this.IsCollidable = true;
            // CHANGE the doors Sprite to locked:
            this.EntitySprite = new Sprite(GameContent.DoorClosed, 0, 0, GameContent.DoorClosed.Width, GameContent.DoorClosed.Height);
            // PLAY the unlock sfx:
            GameContent.DoorUnlock.Play(0.3f, 0.0f, 0.0f);
            // FLAG that the door is now locked:
            _isLocked = true;
        }
        #endregion
    }
}
