using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Interfaces;
using Penumbra;
using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 30/10/21
/// </summary>

namespace Nosocomephobia.Game_Code.Game_Entities
{ 
    public class Flashlight : IUpdatable
    {
        #region FIELDS
        // DECLARE a Light to represent the player light source, call it _light:
        private Light _light;
        private double _lookAngle;
        #endregion

        #region PROPERTIES
        public Light Light
        {
            get { return _light; }
        }
        #endregion

        public Flashlight()
        {
            // INITIALISE the flashlight as a Spotlight:
            _light = new Spotlight();
        }
        
        /// <summary>
        /// METHOD: Sets up the players flashlight with a default size, shadowtype and centres it in the middle of the screen.
        /// </summary>
        public void Initialise()
        {
            // INITIALISE flashlight attributes:
            _light.Scale = new Vector2(1000f);
            _light.ShadowType = ShadowType.Solid;
            _light.Position = new Vector2(Kernel.SCREEN_WIDTH / 2, Kernel.SCREEN_HEIGHT / 2);
        }

        /// <summary>
        /// METHOD: Overload for initalise that allows setting the Flashlight up in a custom location.
        /// </summary>
        /// <param name="scale">Scale of the flashlight.</param>
        /// <param name="shadowType">ShadowType of the flashlight.</param>
        /// <param name="position">Position of the flashlight.</param>
        public void Initialise(Vector2 pScale, ShadowType pShadowType, Vector2 pPosition)
        {
            _light.Scale = pScale;
            _light.ShadowType = pShadowType;
            _light.Position = pPosition;
        }

        /// <summary>
        /// METHOD: Called on each pass of the update loop. Updates the flashlight position and look angle each frame.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // GET the current mouse state:
            MouseState currentMouseState = Mouse.GetState();
            // CALCULATE the angle relative to the mouse pointer and flashlight in radians:
            _lookAngle = Math.Atan2(currentMouseState.Y - _light.Position.Y,
                                         currentMouseState.X - _light.Position.X);
            // SET the rotation of the flashlight so that it faces the mouse cursor:
            _light.Rotation = (float)_lookAngle;
        }
    }
}
