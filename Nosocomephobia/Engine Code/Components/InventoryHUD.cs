using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Verison: 0.3, 20-03-22
/// </summary>
namespace Nosocomephobia.Engine_Code.Components
{
    /// <summary>
    /// Class InventoryHUD
    /// </summary>
    public class InventoryHUD
    {
        #region FIELDS
        // DECLARE a Vector2, call it _location:
        private Vector2 _location;
        // DECLARE a Player, call it _player:
        private Player _player;
        // DECLARE a Texture2D, call it _texture:
        private Texture2D _texture;

        #endregion

        #region PROPERTIES
        /// <summary>
        /// Declare a get-set property for Location
        /// </summary>
        public Vector2 Location
        {
            get { return _location; }
            set { _location = value; }
        }

        /// <summary>
        /// Declare a get-set property for Texture
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor for InventoryHUD.
        /// </summary>
        public InventoryHUD(Texture2D texture, Player player)
        {
            // ASSIGN fields:
            _player = player;
            _texture = texture;
        }

        /// <summary>
        /// Draw method for InventoryHUD. Draws the HUD.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        /// <param name="spriteBatch">The Spritebatch to draw the HUD onto.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // BEGIN the SpriteBatch:
            spriteBatch.Begin();
            // DECLARE a float for the resized texture width and height:
            float newTextureWidth = _texture.Width * 0.4f;
            float newTextureHeight = _texture.Height * 0.4f;
            // DECLARE a Vector2, call it hudLocation and set it so the HUD is central in the display:
            Vector2 hudLocation = new Vector2((Kernel.SCREEN_WIDTH * 0.5f - newTextureWidth * 0.5f), Kernel.SCREEN_HEIGHT - newTextureHeight - 20);
            // DRAW the HUD onto the spriteBatch, set its opacity to 30%:
            spriteBatch.Draw(_texture, new Rectangle((int)(hudLocation.X),
                                                     (int)(hudLocation.Y),
                                                     (int)(newTextureWidth),
                                                     (int)(newTextureHeight)),
                                                     new Color(new Vector3(0.4f)));

            // ITERATE through all of the collected Artefacts in the Player Inventory:
            for(int i = 0; i < _player.Inventory.GetCount(); i++)
            {
                // GET a reference to the texture of the artefact that has been collected:
                Texture2D artefactSpriteTexture = _player.Inventory.Storage[i].EntitySprite.SpriteSheetTexture;
                // DECLARE a Vector2 called artefactSpacing, assign it so that the artefacts will be evenly spaced as they are drawn:
                Vector2 artefactSpacing = new Vector2((newTextureWidth / 8) - artefactSpriteTexture.Width / 2 - 9,
                                                       (newTextureHeight / 2) - artefactSpriteTexture.Height / 2);
                // DECLARE a float called intervalSpacing, this represents the gap between each Artefact in the HUD:
                float intervalSpacing = 127f;
                // DRAW the Artefact into the Inventory HUD Hotbar, using the appropriate spacing:
                spriteBatch.Draw(artefactSpriteTexture,
                                 new Rectangle((int)(hudLocation.X + artefactSpacing.X + i * intervalSpacing),
                                               (int)(hudLocation.Y + artefactSpacing.Y),
                                               (int)(artefactSpriteTexture.Width * 1.1f),
                                               (int)(artefactSpriteTexture.Height * 1.1f)),
                                               Color.White);
            }

            // END the spriteBatch draw sequence:
            spriteBatch.End();
        }

        /// <summary>
        /// Update loop for Hotbar.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        public void Update(GameTime gameTime)
        {
            // do nothing for now
        }
        #endregion
    }
}
