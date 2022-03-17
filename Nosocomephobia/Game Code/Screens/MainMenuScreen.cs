using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Game_Code.Screens;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 17-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Screens
{
    public class MainMenuScreen : Screen
    {
        public MainMenuScreen()
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(GameContent.MENU_BACKGROUND, new Rectangle(0, 0, GameContent.MENU_BACKGROUND.Width, GameContent.MENU_BACKGROUND.Height), Color.White);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Kernel.STATE = State.Game;
            }
        }
    }
}
