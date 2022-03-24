using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.UserEventArgs;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.3, 20-03-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Components
{
    public class Camera : Entity, IInputListener
    {
        #region FIELDS
        // DECLARE a static float, call it MIN_ZOOM_ASPECT and set it to 0.5f:
        private static float MIN_ZOOM_ASPECT = 0.5f;
        // DECLARE a static float, call it MAX_ZOOM_ASPECT and set it to 1.2f:
        private static float MAX_ZOOM_ASPECT = 1.2f;
        // DECLARE a Matrix, call it 'transform':
        private Matrix transform;
        // DECLARE a float, call it 'zoomAspect':
        private float zoomAspect;
        // DECLARE a float, call it 'scrollSpeed':
        private float scrollSpeed;
        // DECLARE a Viewport, call it 'viewport':
        private Viewport viewport;
        // DECLARE a GameEntity, call it 'focusedEntity':
        private GameEntity focusedEntity;

        #endregion
        #region PROPERTIES
        public Matrix Transform
        {
            get { return transform; } // read-only
        }
        #endregion

        /// <summary>
        /// Camera Constructor.
        /// </summary>
        public Camera() : base()
        {
            // INITIALIZE fields:
            transform = new Matrix();
            zoomAspect = 1.0f;
            scrollSpeed = 0.1f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pViewPort">A reference to the viewport.</param>
        public void InjectViewPort(Viewport pViewPort)
        {
            this.viewport = pViewPort;
        }

        /// <summary>
        /// Sets the focus of the _camera onto a specific GameEntity.
        /// </summary>
        /// <param name="entity">The entity to focus the _camera on.</param>
        public void SetFocus(GameEntity entity)
        {
            // SET focusedEntity to the parameter:
            this.focusedEntity = entity;
        }

        #region IMPLEMENTATION OF IUpdatable
        /// <summary>
        /// Default update loop for an IUpdatable.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            // SET up the transform via Matrix:
            transform = Matrix.CreateTranslation(-focusedEntity.EntityLocn.X, -focusedEntity.EntityLocn.Y, 0) * // Main Translation Matrix
                        Matrix.CreateScale(new Vector3(zoomAspect, zoomAspect, 0)) * // Scale Matrix using zoomAspect
                        Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0)); // Origin Offset Matrix
        }
        /// <summary>
        /// Called whenever a mouse input event is fired from the InputManager.
        /// </summary>
        /// <param name="sender">The object firing the event.</param>
        /// <param name="eventInformation">Information about the event.</param>
        public void OnNewMouseInput(object sender, OnMouseInputEventArgs eventInformation)
        {
            // SET zoomAspect to the scollValue in the eventInformation, * scrollSpeed:
            zoomAspect += eventInformation.ScrollValue * scrollSpeed;

            // IF the player has zoomed further than the minimum zoom allowed:
            if (zoomAspect < MIN_ZOOM_ASPECT)
            {
                // RESET the zoom aspect to the minimum value:
                zoomAspect = MIN_ZOOM_ASPECT;
            }
            // IF the player has zoomed further than the maximum zoom allowed:
            if (zoomAspect > MAX_ZOOM_ASPECT)
            {
                // RESET the zoom aspect to the maximum value:
                zoomAspect = MAX_ZOOM_ASPECT;
            }
        }
        #region _
        public void OnNewInput(object sender, OnInputEventArgs eventInformation)
        {
            // nothing
        }

        public void OnKeyReleased(object sender, OnKeyReleasedEventArgs eventInformation)
        {
            // nothing
        }

        public Keys[] getKOI()
        {
            Keys[] fake = new Keys[2];
            return fake;
        }
        #endregion
        #endregion
    }
}
