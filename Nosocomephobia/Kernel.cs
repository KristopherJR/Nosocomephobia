using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.13, 30/10/21
/// 
/// Special thanks to Jaanus Varus for the use of the Penumbra library.
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

        // DECLARE a PenumbraComponent, call it penumbra:
        private PenumbraComponent penumbra;
        // DECLARE a Light to represent the player light source, call it flashlight:
        private Light flashlight;

        public Kernel()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true; 
        }

        protected override void Initialize()
        {
            // INITIALISE the game window:
            this.InitialiseWindow();
            // INITIALISE the flashlight:
            this.InitialiseFlashlight();
            // INTIALISE penumbra as a PenumbraComponent:
            penumbra = new PenumbraComponent(this);
            // ADD the flashlight to the penumbra engine:
            penumbra.Lights.Add(flashlight);
            // ADD penumbra to game components:
            Components.Add(penumbra);
            // CALL penumbras intialize method:
            penumbra.Initialize();
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

        /// <summary>
        /// METHOD: Sets up the players flashlight.
        /// </summary>
        private void InitialiseFlashlight()
        {
            // INITIALISE the flashlight as a Spotlight:
            flashlight = new Spotlight();
            // INITIALISE flashlight attributes:
            flashlight.Scale = new Vector2(1000f);
            flashlight.ShadowType = ShadowType.Solid;
            flashlight.Position = new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // GET the current mouse state:
            MouseState currentMouseState = Mouse.GetState();
            // CALCULATE the position between the mouse pointer and flashlight in radians:
            double lookAngle = Math.Atan2(currentMouseState.Y - flashlight.Position.Y,
                                         currentMouseState.X - flashlight.Position.X);
            // SET the rotation of the flashlight so that it faces the mouse cursor:
            flashlight.Rotation = (float)lookAngle;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // BEGIN penumbras drawing cycle:
            penumbra.BeginDraw();
            // SET the window to dark gray:
            GraphicsDevice.Clear(Color.DarkGray);
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
