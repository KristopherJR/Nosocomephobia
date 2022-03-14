using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 14-03-2022
namespace Nosocomephobia.Game_Code.Game_Entities
{
    public class Artefact : GameEntity, ICollidable
    {
        #region FIELDS
        // DECLARE a boolean flag to determine if this Artefact has been collected:
        private bool _collected;
        #endregion

        #region PROPERTIES
        // property for _collected:
        public bool Collected
        {
            get { return _collected; }
            set { _collected = value; }
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
