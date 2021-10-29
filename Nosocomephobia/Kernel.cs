using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.12, 29/10/21
/// 
/// Special thanks to Jaanus Varus for the use of the Penumbra library.
/// </summary>

namespace Nosocomephobia
{
    public class Kernel : Game
    {
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
            // INITIALISE the window and set it to fullscreen:
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            // INITIALISE the flashlight as a Spotlight:
            flashlight = new Spotlight();
            // INITIALISE flashlight attributes:
            flashlight.Scale = new Vector2(1000f);
            flashlight.ShadowType = ShadowType.Solid;
            flashlight.Position = new Vector2(GraphicsDevice.DisplayMode.Width/2, GraphicsDevice.DisplayMode.Height/2);

            // INTIALISE penumbra as a PenumbraComponent:
            penumbra = new PenumbraComponent(this);
            // ADD the flashlight to the penumbra engine:
            penumbra.Lights.Add(flashlight);
            // ADD penumbra to game components:
            Components.Add(penumbra);

            // CALL penumbras intialize method:
            penumbra.Initialize();
            base.Initialize();

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
