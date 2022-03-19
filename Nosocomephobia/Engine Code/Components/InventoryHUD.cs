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
/// Verison: 0.1, 19-03-22
/// </summary>
namespace Nosocomephobia.Engine_Code.Components
{
    public class InventoryHUD : Component
    {
        #region FIELDS
        private Texture2D _texture;
        private Vector2 _location;
        private Player _player;
        private Camera _camera;

        #endregion

        #region PROPERTIES
        public Vector2 Location
        {
            get { return _location; }
            set { _location = value; }
        }

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
        public InventoryHUD(Texture2D texture, Player player, Camera camera)
        {
            _texture = texture;
            _player = player;
            _camera = camera;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // GET the cameras transform:
            _camera.Transform.Decompose(out Vector3 scaley, out _, out Vector3 trans);
            // CONVERT the Vector3 translation to a Vector2 (discard Z):
            Vector2 translation = new Vector2(Math.Abs(trans.X), Math.Abs(trans.Y));
            // CONVERT the Vector3 scale to a Vector2 (discard Z):
            Vector2 scale = new Vector2(scaley.X, scaley.Y);

            Vector2 offset = new Vector2((Kernel.SCREEN_WIDTH / 2) - (_texture.Width/2), Kernel.SCREEN_HEIGHT);

            Debug.WriteLine(translation);
            spriteBatch.Draw(_texture, new Rectangle((int)((translation.X / scale.X) + offset.X),
                                                     (int)((translation.Y / scale.Y) + offset.Y),
                                                     (int)(_texture.Width / scale.X),
                                                     (int)(_texture.Height / scale.Y)),
                                                     Color.White);

            for(int i = 0; i < _player.Inventory.GetCount(); i++)
            {
                spriteBatch.Draw(_player.Inventory.Storage[i].EntitySprite.SpriteSheetTexture,
                                 new Rectangle((int)_player.EntityLocn.X,
                                               (int)_player.EntityLocn.Y,
                                               _texture.Width,
                                               _texture.Height),
                                               Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        #endregion
    }
}
