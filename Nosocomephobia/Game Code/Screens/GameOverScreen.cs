using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Components;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.3, 24-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Screens
{
    /// <summary>
    /// Class GameOverScreen
    /// </summary>
    public class GameOverScreen : Screen
    {
        #region FIELDS
        // DECLARE a Dictionary<string, Component>, call it _components:
        private Dictionary<string, Component> _components;
        // DECLARE a bool, call it _quitHovered:
        private bool _quitHovered;
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor for GameOverScreen
        /// </summary>
        public GameOverScreen()
        {
            // DECLARE a Button, call it quitGameButton and set its position and click handler:
            Button quitGameButton = new Button(GameContent.QuitButtonWhite, GameContent.Font);
            quitGameButton.Position = new Vector2(5, 480);
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
        /// Draw method for GameOverScreen
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime</param>
        /// <param name="spriteBatch">The SpriteBatch to draw the screen onto.</param>
        /// <param name="graphicsDevice">The graphics device to draw the spritebatch onto.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            // BEGIN the spritebatch:
            spriteBatch.Begin();
            // DRAW all of the GameOverScreen components:
            spriteBatch.Draw(GameContent.GameOverBackground, new Rectangle(0, 0, GameContent.GameOverBackground.Width, GameContent.GameOverBackground.Height), Color.White);
            spriteBatch.Draw(GameContent.GameOverTitle, new Rectangle(30, 30, GameContent.GameOverTitle.Width, GameContent.GameOverTitle.Height), Color.White);
            spriteBatch.DrawString(GameContent.Font, "Looks like your lantern went out...", new Vector2(30, 280), Color.White);
            // CHECK if the player only scored 1:
            if(Kernel.SCORE == 1)
            {
                // IF they did change "artefacts" to "artefact" (non-plural):
                spriteBatch.DrawString(GameContent.Font, "You collected " + Kernel.SCORE + " Artefact", new Vector2(30, 330), Color.White);
            }
            else
            {
                // ELSE set it to plural ("artefacts"):
                spriteBatch.DrawString(GameContent.Font, "You collected " + Kernel.SCORE + " Artefacts", new Vector2(30, 330), Color.White);
            }
            spriteBatch.DrawString(GameContent.Font, "(Remember there is one per quadrant)", new Vector2(30, 380), Color.White);
            foreach (KeyValuePair<string, Component> component in _components)
            {
                component.Value.Draw(gameTime, spriteBatch);
            }
            // END the spritebatch:
            spriteBatch.End();
        }

        /// <summary>
        /// Update loop for GameOverScreen.
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
                        (component.Value as Button).Texture = GameContent.QuitButtonHoveredWhite;
                    }
                }
                else
                {
                    // ELSE set it to non-hovered:
                    if (component.Key == "quit_game_button")
                    {
                        (component.Value as Button).Texture = GameContent.QuitButtonWhite;
                        _quitHovered = false;
                    }
                }
            }
        }
        #endregion
    }
}
