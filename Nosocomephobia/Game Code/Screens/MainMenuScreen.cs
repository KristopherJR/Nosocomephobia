using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Components;
using Nosocomephobia.Game_Code.Screens;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.4, 17-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Screens
{
    public class MainMenuScreen : Screen
    {
        private Dictionary<string, Component> _components;
        public MainMenuScreen()
        {


            Button startGameButton = new Button(GameContent.StartButton, GameContent.Font);
            startGameButton.Position = new Vector2(0, 300);
            startGameButton.Click += StartGameButton_Click;

            Button quitGameButton = new Button(GameContent.QuitButton, GameContent.Font);
            quitGameButton.Position = new Vector2(0, 450);
            quitGameButton.Click += QuitGameButton_Click;

            _components = new Dictionary<string, Component>();
            _components.Add("start_game_button", startGameButton);
            _components.Add("quit_game_button", quitGameButton);
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            Kernel.STATE = State.Game;
            Kernel.BackgroundMusic.Stop();
            SoundEffectInstance gameMusic = GameContent.BackgroundGame.CreateInstance();
            gameMusic.IsLooped = true;
            gameMusic.Volume = 0.3f;
            gameMusic.Play();
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            Kernel.RUNNING = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(GameContent.MenuBackground, new Rectangle(0, 0, GameContent.MenuBackground.Width, GameContent.MenuBackground.Height), Color.White);
            spriteBatch.Draw(GameContent.MenuTitle, new Rectangle(0, 30, GameContent.MenuTitle.Width, GameContent.MenuTitle.Height), Color.White);

            foreach (KeyValuePair<string, Component> component in _components)
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
                    if (component.Key == "quit_game_button")
                    {
                        (component.Value as Button).Texture = GameContent.QuitButtonHovered;
                    }
                }
                else
                {
                    if (component.Key == "start_game_button")
                    {
                        (component.Value as Button).Texture = GameContent.StartButton;
                    }
                    if (component.Key == "quit_game_button")
                    {
                        (component.Value as Button).Texture = GameContent.QuitButton;
                    }
                }
            }
        }
    }
}
