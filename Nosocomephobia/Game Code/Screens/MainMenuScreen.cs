using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Components;
using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.7, 24-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Screens
{
    /// <summary>
    /// Class MainMenuScreen
    /// </summary>
    public class MainMenuScreen : Screen
    {
        #region FIELDS
        // DECLARE a Dictionary<string, Component>, call it _mainMenuComponents:
        private Dictionary<string, Component> _mainMenuComponents;
        // DECLARE a Dictionary<string, Component>, call it _howToPlayComponents:
        private Dictionary<string, Component> _howToPlayComponents;
        // DECLARE a Dictionary<string, Component>, call it _viewMapComponents:
        private Dictionary<string, Component> _viewMapComponents;
        // DECLARE a bool, call it _startHovered:
        private bool _startHovered;
        // DECLARE a bool, call it _howToPlayHovered:
        private bool _howToPlayHovered;
        // DECLARE a bool, call it _quitHovered:
        private bool _quitHovered;
        // DECLARE a bool, call it _backHovered:
        private bool _backHovered;
        // DECLARE a bool, call it _back2Hovered:
        private bool _back2Hovered;
        // DECLARE a bool, call it _viewMapHovered:
        private bool _viewMapHovered;
        // DECLARE a bool, call it _drawMainMenu:
        private bool _drawMainMenu;
        // DECLARE a bool, call it _drawGuide:
        private bool _drawGuide;
        // DECLARE a bool, call it _drawMap:
        private bool _drawMap;
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor for MainMenuScreen
        /// </summary>
        public MainMenuScreen()
        {
            // DECLARE a Button, call it startGameButton and assign its position and click event handler:
            Button startGameButton = new Button(GameContent.StartButton, GameContent.Font);
            startGameButton.Position = new Vector2(15, 300);
            startGameButton.Click += StartGameButton_Click;

            // DECLARE a Button, call it howToPlayButton and assign its position and click event handler:
            Button howToPlayButton = new Button(GameContent.HowToPlayButton, GameContent.Font);
            howToPlayButton.Position = new Vector2(49, 450);
            howToPlayButton.Click += HowToPlayButton_Click;

            // DECLARE a Button, call it quitGameButton and assign its position and click event handler:
            Button quitGameButton = new Button(GameContent.QuitButton, GameContent.Font);
            quitGameButton.Position = new Vector2(28, 600);
            quitGameButton.Click += QuitGameButton_Click;

            // DECLARE a Button, call it backButton and assign its position and click event handler:
            Button backButton = new Button(GameContent.BackButton, GameContent.Font);
            backButton.Position = new Vector2(28, 800);
            backButton.Click += BackButton_Click;

            // DECLARE a Button, call it backButton2 and assign its position and click event handler:
            Button backButton2 = new Button(GameContent.BackButtonWhite, GameContent.Font);
            backButton2.Position = new Vector2(28, 800);
            backButton2.Click += BackButton2_Click;

            // DECLARE a Button, call it viewMapButton and assign its position and click event handler:
            Button viewMapButton = new Button(GameContent.ViewMapButton, GameContent.Font);
            viewMapButton.Position = new Vector2(400, 800);
            viewMapButton.Click += ViewMapButton_Click;

            // INITIALISE the Dictionaries:
            _mainMenuComponents = new Dictionary<string, Component>();
            _howToPlayComponents = new Dictionary<string, Component>();
            _viewMapComponents = new Dictionary<string, Component>();

            // ADD the main menu components to their dictionary:
            _mainMenuComponents.Add("start_game_button", startGameButton);
            _mainMenuComponents.Add("how_to_play_button", howToPlayButton);
            _mainMenuComponents.Add("quit_game_button", quitGameButton);

            // ADD the how to play components to their dictionary:
            _howToPlayComponents.Add("back_button", backButton);
            _howToPlayComponents.Add("view_map_button", viewMapButton);

            // ADD the view map components to their dictionary:
            _viewMapComponents.Add("back_button_2", backButton2);

            // SET all bools to false apart from _drawMainMenu:
            _startHovered = false;
            _howToPlayHovered = false;
            _quitHovered = false;
            _backHovered = false;
            _back2Hovered = false;
            _viewMapHovered = false;
            _drawMainMenu = true;
            _drawGuide = false;
            _drawMap = false;
        }

        /// <summary>
        /// StartGameButton Click event handler.
        /// </summary>
        /// <param name="sender">the object sending the event.</param>
        /// <param name="e">Event information.</param>
        private void StartGameButton_Click(object sender, EventArgs e)
        {
            // SET the games state in Kernel to start the game:
            Kernel.STATE = State.Game;
            // STOP the main menu music:
            Kernel.BackgroundMusic.Stop();
            // START the game music:
            SoundEffectInstance gameMusic = GameContent.BackgroundGame.CreateInstance();
            // LOOP the game music and set its volume:
            gameMusic.IsLooped = true;
            gameMusic.Volume = 0.6f;
            gameMusic.Play();
        }
        /// <summary>
        /// HowToPlayButton Click event handler.
        /// </summary>
        /// <param name="sender">the object sending the event.</param>
        /// <param name="e">Event information.</param>
        private void HowToPlayButton_Click(object sender, EventArgs e)
        {
            // SET _drawGuide to true, and the others to false:
            _drawGuide = true;
            _drawMainMenu = false;
            _drawMap = false;
        }
        /// <summary>
        /// QuitGameButton Click event handler.
        /// </summary>
        /// <param name="sender">the object sending the event.</param>
        /// <param name="e">Event information.</param>
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            // SET RUNNING to false:
            Kernel.RUNNING = false;
        }
        /// <summary>
        /// BackButton Click event handler.
        /// </summary>
        /// <param name="sender">the object sending the event.</param>
        /// <param name="e">Event information.</param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            // SET _drawMainMenu to true, and the others to false:
            (_mainMenuComponents["how_to_play_button"] as Button).Texture = GameContent.HowToPlayButton;
            _drawGuide = false;
            _drawMainMenu = true;
            _drawMap = false;
        }
        /// <summary>
        /// BackButton2 Click event handler.
        /// </summary>
        /// <param name="sender">the object sending the event.</param>
        /// <param name="e">Event information.</param>
        private void BackButton2_Click(object sender, EventArgs e)
        {
            // SET _drawGuide to true, and the others to false:
            _drawGuide = true;
            _drawMainMenu = false;
            _drawMap = false;
        }
        /// <summary>
        /// ViewMapButton Click event handler.
        /// </summary>
        /// <param name="sender">the object sending the event.</param>
        /// <param name="e">Event information.</param>
        private void ViewMapButton_Click(object sender, EventArgs e)
        {
            // SET _drawMap to true, and the others to false:
            _drawGuide = false;
            _drawMainMenu = false;
            _drawMap = true;
        }

        /// <summary>
        /// Draw method for MainMenuScreen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        /// <param name="spriteBatch">The SpriteBatch to draw the menu onto.</param>
        /// <param name="graphicsDevice">The graphics device to draw the spritebatch onto.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            // START the spritebatch:
            spriteBatch.Begin();
            // IF _drawMainMenu is true:
            if (_drawMainMenu)
            {
                // DRAW the main menu components:
                spriteBatch.Draw(GameContent.MenuBackground, new Rectangle(0, 0, GameContent.MenuBackground.Width, GameContent.MenuBackground.Height), Color.White);
                spriteBatch.Draw(GameContent.MenuTitle, new Rectangle(40, 30, GameContent.MenuTitle.Width, GameContent.MenuTitle.Height), Color.White);

                foreach (KeyValuePair<string, Component> component in _mainMenuComponents)
                {

                    component.Value.Draw(gameTime, spriteBatch);
                }
            }
            // IF _drawGuide is true:
            if (_drawGuide)
            {
                // DRAW the guide components:
                spriteBatch.Draw(GameContent.MenuBackground, new Rectangle(0, 0, GameContent.MenuBackground.Width, GameContent.MenuBackground.Height), Color.White);
                spriteBatch.Draw(GameContent.MenuTitle, new Rectangle(40, 30, GameContent.MenuTitle.Width, GameContent.MenuTitle.Height), Color.White);
                spriteBatch.Draw(GameContent.HowToPlayInfo, new Rectangle(0, 0, GameContent.HowToPlayInfo.Width, GameContent.HowToPlayInfo.Height), Color.White);
                spriteBatch.Draw(GameContent.HTPTitle, new Rectangle(15, 200, GameContent.HTPTitle.Width, GameContent.HTPTitle.Height), Color.White);

                foreach (KeyValuePair<string, Component> component in _howToPlayComponents)
                {

                    component.Value.Draw(gameTime, spriteBatch);
                }
            }
            // IF _drawGuide is true:
            if (_drawMap)
            {
                // DRAW the map components:
                spriteBatch.Draw(GameContent.LevelLayoutGuide, new Rectangle(0, 0, GameContent.MenuBackground.Width, GameContent.MenuBackground.Height), Color.White);

                foreach (KeyValuePair<string, Component> component in _viewMapComponents)
                {

                    component.Value.Draw(gameTime, spriteBatch);
                }

            }
            // END the spritebatch:
            spriteBatch.End();
        }

        /// <summary>
        /// Update method for MainMenuScreen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            // IF _drawMainMenu is true:
            if (_drawMainMenu)
            {
                // UPDATE the main menu components:
                foreach (KeyValuePair<string, Component> component in _mainMenuComponents)
                {
                    component.Value.Update(gameTime);

                    if ((component.Value as Button).IsHovering)
                    {
                        if (component.Key == "start_game_button")
                        {
                            if (!_startHovered)
                            {
                                GameContent.PageTurn.Play(0.2f, 0.0f, 0.0f);
                                _startHovered = true;
                            }
                            (component.Value as Button).Texture = GameContent.StartButtonHovered;

                        }
                        if (component.Key == "how_to_play_button")
                        {
                            if (!_howToPlayHovered)
                            {
                                GameContent.PageTurn.Play(0.2f, 0.0f, 0.0f);
                                _howToPlayHovered = true;
                            }
                            (component.Value as Button).Texture = GameContent.HowToPlayButtonHovered;

                        }
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
                        if (component.Key == "start_game_button")
                        {
                            (component.Value as Button).Texture = GameContent.StartButton;
                            _startHovered = false;
                        }
                        if (component.Key == "how_to_play_button")
                        {
                            (component.Value as Button).Texture = GameContent.HowToPlayButton;
                            _howToPlayHovered = false;
                        }
                        if (component.Key == "quit_game_button")
                        {
                            (component.Value as Button).Texture = GameContent.QuitButton;
                            _quitHovered = false;
                        }
                    }
                }
            }
            // IF _drawGuide is true:
            if (_drawGuide)
            {
                // UPDATE the guide components:
                foreach (KeyValuePair<string, Component> component in _howToPlayComponents)
                {
                    {
                        component.Value.Update(gameTime);

                        if ((component.Value as Button).IsHovering)
                        {
                            if (component.Key == "back_button")
                            {
                                if (!_backHovered)
                                {
                                    GameContent.PageTurn.Play(0.2f, 0.0f, 0.0f);
                                    _backHovered = true;
                                }
                            (component.Value as Button).Texture = GameContent.BackButtonHovered;

                            }
                            if (component.Key == "view_map_button")
                            {
                                if (!_viewMapHovered)
                                {
                                    GameContent.PageTurn.Play(0.2f, 0.0f, 0.0f);
                                    _viewMapHovered = true;
                                }
                                (component.Value as Button).Texture = GameContent.ViewMapButtonHovered;

                            }
                        }
                        else
                        {
                            if (component.Key == "back_button")
                            {
                                (component.Value as Button).Texture = GameContent.BackButton;
                                _backHovered = false;
                            }
                            if (component.Key == "view_map_button")
                            {
                                (component.Value as Button).Texture = GameContent.ViewMapButton;
                                _viewMapHovered = false;
                            }
                        }
                    }
                }
            }
            // IF _drawMap is true:
            if (_drawMap)
            {
                // UPDATE the map components:
                foreach (KeyValuePair<string, Component> component in _viewMapComponents)
                {
                    component.Value.Update(gameTime);

                    if ((component.Value as Button).IsHovering)
                    {
                        if (component.Key == "back_button_2")
                        {
                            if (!_back2Hovered)
                            {
                                GameContent.PageTurn.Play(0.2f, 0.0f, 0.0f);
                                _back2Hovered = true;
                            }
                            (component.Value as Button).Texture = GameContent.BackButtonHoveredWhite;

                        }

                    }
                    else
                    {
                        if (component.Key == "back_button_2")
                        {
                            (component.Value as Button).Texture = GameContent.BackButtonWhite;
                            _back2Hovered = false;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
