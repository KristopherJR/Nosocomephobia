using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Components;
using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.3, 24-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Screens
{
    /// <summary>
    /// Class VictoryScreen
    /// </summary>
    public class VictoryScreen : Screen
    {
        #region FIELDS
        // DECLARE a Dictionary<string, Component>, call it _components:
        private Dictionary<string, Component> _components;
        // DECLARE a bool, call it _quitHovered:
        private bool _quitHovered;
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor for VictoryScreen
        /// </summary>
        public VictoryScreen()
        {
            // DECLARE a Button, call it quitGameButton and set its position and click handler:
            Button quitGameButton = new Button(GameContent.QuitButton, GameContent.Font);
            quitGameButton.Position = new Vector2(30, 550);
            quitGameButton.Click += QuitGameButton_Click;

            // INITIALISE _components and add the new Button:
            _components = new Dictionary<string, Component>();
            _components.Add("quit_game_button", quitGameButton);

            // SET _quitHovered to false by default:
            _quitHovered = false;
        }

        /// <summary>
        /// Click event handler for QuitGameButton.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">Event information.</param>
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            // SET RUNNING to false:
            Kernel.RUNNING = false;
        }

        /// <summary>
        /// Draw method for VictoryScreen
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime</param>
        /// <param name="spriteBatch">The SpriteBatch to draw the screen onto.</param>
        /// <param name="graphicsDevice">The graphics device to draw the spritebatch onto.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            // BEGIN the spritebatch:
            spriteBatch.Begin();
            // DRAW all of the VictoryScreen components:
            spriteBatch.Draw(GameContent.MenuBackground, new Rectangle(0, 0, GameContent.MenuBackground.Width, GameContent.MenuBackground.Height), Color.White);
            spriteBatch.Draw(GameContent.MenuTitle, new Rectangle(30, 30, GameContent.MenuTitle.Width, GameContent.MenuTitle.Height), Color.White);
            spriteBatch.Draw(GameContent.VictoryTitle, new Rectangle(30, 200, GameContent.VictoryTitle.Width, GameContent.VictoryTitle.Height), Color.White);


            spriteBatch.DrawString(GameContent.Font, "Congratulations!", new Vector2(60, 330), Color.Black);
            spriteBatch.DrawString(GameContent.Font, "You have collected all 4 of the Artefacts", new Vector2(60, 390), Color.Black);
            spriteBatch.DrawString(GameContent.Font, "May you never return...", new Vector2(60, 450), Color.Black);
            foreach (KeyValuePair<string, Component> component in _components)
            {
                component.Value.Draw(gameTime, spriteBatch);
            }
            // END the spritebatch:
            spriteBatch.End();
        }
        /// <summary>
        /// Update loop for VictoryScreen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            // ITERATE through all _components:
            foreach (KeyValuePair<string, Component> component in _components)
            {
                // UPDATE each component:
                component.Value.Update(gameTime);
                // IF the button is being hovered:
                if ((component.Value as Button).IsHovering)
                {
                    // CHANGE the texture of the button and play a SFX:
                    if (component.Key == "quit_game_button")
                    {
                        if (!_quitHovered)
                        {
                            GameContent.PageTurn.Play(0.2f, 0.0f, 0.0f);
                            _quitHovered = true;
                        }
                        (component.Value as Button).Texture = GameContent.QuitButtonHovered;
                    }
                }
                else
                {
                    // ELSE set it to non-hovered:
                    if (component.Key == "quit_game_button")
                    {
                        (component.Value as Button).Texture = GameContent.QuitButton;
                        _quitHovered = false;
                    }
                }
            }
        }
        #endregion
    }
}
