using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities;
using Penumbra;
using System;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.13, 30/10/21
/// 
/// Penumbra Author: Jaanus Varus
/// </summary>

namespace Nosocomephobia
{
    public class Kernel : Game
    {
        // DECLARE a public static int to represent the Screen Width, call it 'SCREEN_WIDTH':
        public static int SCREEN_WIDTH;
        // DECLARE a public static int to represent the Screen Height, call it 'SCREEN_HEIGHT':
        public static int SCREEN_HEIGHT;

        // DECLARE a bool to toggle between full screen and windowed for development purposes:
        private bool _devMode = true;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // DECLARE a PenumbraComponent, call it _penumbra:
        private PenumbraComponent _penumbra;

        // DECLARE an Flashlight, call it _flashlight:
        private Flashlight _flashlight;
        
        public Kernel()
        {
            _graphics = new GraphicsDeviceManager(this);
            // INITIALISE _flashlight:
            _flashlight = new Flashlight();
            Content.RootDirectory = "Content";
            IsMouseVisible = true; 
        }

        protected override void Initialize()
        {
            // INITIALISE the game window:
            this.InitialiseWindow();
            // INITIALISE the flashlight:
            _flashlight.Initialise();
            // INTIALISE penumbra as a PenumbraComponent:
            _penumbra = new PenumbraComponent(this);
            // ADD the flashlight to the penumbra engine:
            _penumbra.Lights.Add(_flashlight.Light);
            // ADD penumbra to game components:
            Components.Add(_penumbra);
            // CALL penumbras intialize method:
            _penumbra.Initialize();
            // INITALISE the base class:
            base.Initialize();
        }

        /// <summary>
        /// METHOD: Sets up the game window to windowed view if Dev Mode is enabled, or full screen if Dev Mode is disabled.
        /// </summary>
        private void InitialiseWindow()
        {
            // INITIALISE the window and set it to fullscreen if devMode is disabled. If devMode is enabled, set to windowed view:
            if (_devMode)
            {
                _graphics.PreferredBackBufferHeight = 900;
                _graphics.PreferredBackBufferWidth = 1600;
                _graphics.ApplyChanges();
            }
            else
            {
                _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                _graphics.IsFullScreen = true;
                _graphics.ApplyChanges();
            }
            // STORE the windows new width and height in the public static int fields for ease of access later:
            SCREEN_WIDTH = GraphicsDevice.Viewport.Width;
            SCREEN_HEIGHT = GraphicsDevice.Viewport.Height;
        }

        

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _flashlight.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // BEGIN penumbras drawing cycle:
            _penumbra.BeginDraw();
            // SET the window to dark gray:
            GraphicsDevice.Clear(Color.DarkGray);

            base.Draw(gameTime);
        }
    }
}
