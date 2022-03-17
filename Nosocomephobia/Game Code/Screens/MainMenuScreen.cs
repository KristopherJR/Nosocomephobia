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
/// Version: 0.2, 17-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Screens
{
    public class MainMenuScreen : Screen
    {
        private Dictionary<string, Component> _components;
        public MainMenuScreen()
        {
            Button startGameButton = new Button(GameContent.StartButton, GameContent.Font);
            startGameButton.Position = new Vector2(75, 100);
            startGameButton.Click += StartGameButton_Click;

            _components = new Dictionary<string, Component>();
            _components.Add("start_game_button", startGameButton);
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            Kernel.STATE = State.Game;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(GameContent.MenuBackground, new Rectangle(0, 0, GameContent.MenuBackground.Width, GameContent.MenuBackground.Height), Color.White);
            
            foreach(KeyValuePair<string, Component> component in _components)
            {
                component.Value.Draw(gameTime, spriteBatch);
            }
            
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, Component> component in _components)
            {
                component.Value.Update(gameTime);

                if ((component.Value as Button).IsHovering)
                {
                    if(component.Key == "start_game_button")
                    {
                        (component.Value as Button).Texture = GameContent.StartButtonHovered; 
                    }
                }
                else
                {
                    if (component.Key == "start_game_button")
                    {
                        (component.Value as Button).Texture = GameContent.StartButton;
                    }
                }
            }
        }
    }
}
