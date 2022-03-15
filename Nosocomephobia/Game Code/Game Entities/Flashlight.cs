using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Components;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using Penumbra;
using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.3, 15-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Game_Entities
{ 
    public class Flashlight : GameEntity
    {
        #region FIELDS
        // DECLARE a Light to represent the player light source, call it _light:
        private Light _light;
        private double _lookAngle;
        private GameEntity _focusedEntity;
        private Camera _gameCamera;
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
        public void Initialise(Camera c)
        {
            _gameCamera = c;
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
        /// Sets the focus of the _flashlight onto a specific GameEntity.
        /// </summary>
        /// <param name="entity">The entity to focus the _flashlight on.</param>
        public void SetFocus(GameEntity entity)
        {
            // SET focusedEntity to the parameter:
            this._focusedEntity = entity;
        }
        
        /// <summary>
        /// Uses the Cameras position to gets the mouse position in world space.
        /// </summary>
        /// <returns></returns>
        private Vector2 GetMouseWorldPosition()
        {
            // GET the cameras transform:
            _gameCamera.Transform.Decompose(out Vector3 scaley, out _, out Vector3 trans);
            // CONVERT the Vector3 translation to a Vector2 (discard Z):
            Vector2 translation = new Vector2(trans.X, trans.Y);
            // CONVERT the Vector3 scale to a Vector2 (discard Z):
            Vector2 scale = new Vector2(scaley.X, scaley.Y);
            // GET the current mouse state:
            MouseState currentMouseState = Mouse.GetState();
            // STORE the x and y of the mouse state in a Vector2:
            Vector2 worldSpaceMousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            // APPLY translation:
            worldSpaceMousePosition -= translation;
            worldSpaceMousePosition /= scale;

            return worldSpaceMousePosition;
        }

        /// <summary>
        /// Update loop for Flashlight, overrides the parent Update() method. Updates the flashlight position and look angle each frame.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            Vector2 torchOriginOffest = new Vector2();

            // VERIFY type safety:
            if (_focusedEntity is Player)
            {
                // player walking up
                if((_focusedEntity as Player).WalkDirection == Kernel.UP)
                {
                    torchOriginOffest = new Vector2(-20,-50);
                }
                // player walking down
                if ((_focusedEntity as Player).WalkDirection == Kernel.DOWN)
                {
                    torchOriginOffest = new Vector2(13,-20);
                }
                // player walking left
                if ((_focusedEntity as Player).WalkDirection == Kernel.LEFT)
                {
                    torchOriginOffest = new Vector2(-26,-18);
                }
                // player walking right
                if ((_focusedEntity as Player).WalkDirection == Kernel.RIGHT)
                {
                    torchOriginOffest = new Vector2(27,-18);
                }

                Vector2 playerCentre = new Vector2(_focusedEntity.EntityLocn.X + (_focusedEntity.EntitySprite.TextureWidth / 2),
                                                   _focusedEntity.EntityLocn.Y + (_focusedEntity.EntitySprite.TextureHeight / 2));

                // SET the flashlights position to the player centre plus offset:
                _light.Position = playerCentre += torchOriginOffest;

                Vector2 worldSpaceMousePosition = GetMouseWorldPosition();

                // CALCULATE the angle relative to the mouse pointer and flashlight in radians:
                _lookAngle = Math.Atan2(worldSpaceMousePosition.Y - _light.Position.Y,
                                        worldSpaceMousePosition.X - _light.Position.X);
                // SET the rotation of the flashlight so that it faces the mouse cursor:
                _light.Rotation = (float)_lookAngle;
            }

        }
    }
}
