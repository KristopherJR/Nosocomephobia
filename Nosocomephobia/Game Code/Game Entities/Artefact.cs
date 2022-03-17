using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.3, 17-03-2022
namespace Nosocomephobia.Game_Code.Game_Entities
{
    public class Artefact : GameEntity, ICollidable
    {
        #region FIELDS
        // DECLARE a boolean flag to determine if this Artefact has been collected:
        private bool _collected;
        // DECLARE a SoundEffect, call it _pickupSFX:
        private SoundEffect _pickupSFX;
        #endregion

        #region PROPERTIES
        // property for _collected:
        public bool Collected
        {
            get { return _collected; }
            set { _collected = value; }
        }
        // property for _pickupSFX:
        public SoundEffect PickupSFX
        {
            get { return _pickupSFX; }
            set { _pickupSFX = value; }
        }
        #endregion
        /// <summary>
        /// Constructor for Artefact
        /// </summary>
        public Artefact()
        {
            // SET the Artefact to collected as false by default:
            _collected = false;
            // SET the Artefact as Collidable so the CollisionManager listens for collisions:
            isCollidable = true;
        }
    }
}
