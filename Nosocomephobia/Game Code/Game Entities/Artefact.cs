using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 14-03-2022
namespace Nosocomephobia.Game_Code.Game_Entities
{
    public class Artefact : GameEntity, ICollidable, ICollisionResponder
    {
        #region FIELDS
        private bool _collected;
        #endregion
        /// <summary>
        /// Constructor for Artefact
        /// </summary>
        public Artefact()
        {
            // SET the Artefact to collected as false by default:
            _collected = false;
        }

        /// <summary>
        /// Called when a collision happens, tells the object how to react.
        /// </summary>
        /// <param name="collidee">The object that this object collided into.</param>
        public void CheckAndRespond(ICollidable collidee)
        {
            // IF the Player has ran into the Artefact
            if((collidee as Entity).UName == "Player1")
            {
                // ADD the artefact to the player inventory
                (collidee as Player).Inventory.Add(this.UName, this);
                // FLAG the artefact for removal from the scene by setting it as collected:
                _collected = true;
            }
        }
    }
}
