using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 17-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Screens
{
    /// <summary>
    /// Abstract class Screen. Inherited by all GUI screens.
    /// </summary>
    public abstract class Screen
    {
        // default update method for a screen
        public abstract void Update(GameTime gameTime);
        // default draw method for a screen
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
    }
}
