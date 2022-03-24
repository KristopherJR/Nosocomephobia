using Microsoft.Xna.Framework;
using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 15-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.UserEventArgs
{
    public class OnUpdateEventArgs : EventArgs
    {
        #region FIELDS
        // DECLARE a reference to GameTime, call it _gameTime:
        private GameTime _gametime;
        #endregion FIELDS

        #region PROPERTIES
        // DECLARE a get-set property for GameTime:
        public GameTime GameTime
        {
            get { return _gametime; }
        }
        #endregion PROPERTIES

        #region METHODS
        /// <summary>
        /// Constructor for UpdateEventArgs
        /// </summary>
        /// <param name="pGameTime">A reference to the GameTime.</param>
        public OnUpdateEventArgs(GameTime pGameTime)
        {
            // INITALISE _gameTime:
            _gametime = pGameTime;
        }

        #endregion METHODS
    }
}
