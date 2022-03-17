using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Components;
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
        private List<Component> _components;
        public MainMenuScreen()
        {
            Button startGameButton = new Button(GameContent.StartButton, GameContent.Font);
            startGameButton.Position = new Vector2(300, 300);
            startGameButton.Click += StartGameButton_Click;

            _components = new List<Component>()
            {
                startGameButton
            };
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            Kernel.STATE = State.Game;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(GameContent.MenuBackground, new Rectangle(0, 0, GameContent.MenuBackground.Width, GameContent.MenuBackground.Height), Color.White);
            
            foreach(Component component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(Component component in _components)
            {
                component.Update(gameTime);
            }
        }
    }
}
