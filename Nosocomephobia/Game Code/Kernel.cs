using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Camera;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.Managers;
using Nosocomephobia.Game_Code;
using Nosocomephobia.Game_Code.Game_Entities;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using Nosocomephobia.Game_Code.World;
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
        // DECLARE a public static Boolean, call it 'RUNNING':
        public static Boolean RUNNING;
        // DECLARE a public static int to represent the Screen Width, call it 'SCREEN_WIDTH':
        public static int SCREEN_WIDTH;
        // DECLARE a public static int to represent the Screen Height, call it 'SCREEN_HEIGHT':
        public static int SCREEN_HEIGHT;

        // DECLARE a bool to toggle between full screen and windowed for development purposes:
        private bool _devMode = true;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // DECLARE an EntityManager, call it '_eManager'. Store it as its interface IEntityManager:
        private IEntityManager _eManager;
        // DECLARE a SceneManager, call it '_sManager'. Store it as its interface ISceneManager:
        private ISceneManager _sManager;
        // DECLARE a CollisionManager, call it '_cManager'. Store it as its interface ICollisionManager:
        private ICollisionManager _cManager;
        // DECLARE an InputManager, call it '_iManager'. Store it as its interface IInputManager:
        private IInputManager _iManager;
        // DECLARE an NavigationManager, call it '_nManager'. Store it as its interface INavigationManager:
        private INavigationManager _nManager;

        // DECLARE a Camera, call it '_camera':
        private Camera _camera;

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

            // INITIALIZE the Managers:
            _eManager = new EntityManager();
            _sManager = new SceneManager();
            _cManager = new CollisionManager();
            _iManager = new InputManager();
            _nManager = new NavigationManager();

            // INITIALIZE the camera:
            _camera = new Camera(GraphicsDevice.Viewport);

            // SUBSCRIBE the camera to listen for input events:
            _iManager.Subscribe(_camera,
                                _camera.OnNewInput,
                                _camera.OnKeyReleased,
                                _camera.OnNewMouseInput);

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
            // SET RUNNING to true:
            RUNNING = true;
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
        /// SpawnObjects is used to spawn all game objects into the SceneGraph/EntityPool/CollisionMap. Called in Kernel from LoadContent().
        /// </summary>


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // LOADING the game content:
            GameContent.LoadContent(Content);
            // SPAWN the game objects:
            this.SpawnObjects();
        }

        private void SpawnObjects()
        {
            // REQUEST a new 'Player' object from the EntityManager, and pass it to the SceneManager. Call it Sam.:
            IEntity _player = _eManager.createEntity<Player>();
            // SPAWN _player into the SceneGraph:
            _sManager.spawn(_player);


            // SET _camera focus onto Player:
            _camera.SetFocus(_player as GameEntity);
            // SET _flashlight focus onto Player:
            _flashlight.SetFocus(_player as GameEntity);

            // ITERATE through the SceneGraph:
            for (int i = 0; i < _sManager.SceneGraph.Count; i++)
            {
                if (_sManager.SceneGraph[i] is Player)
                {
                    // SUBSCRIBE the paddle to listen for input events and key release events:
                    _iManager.Subscribe((_sManager.SceneGraph[0] as IInputListener),
                                       (_sManager.SceneGraph[0] as Player).OnNewInput,
                                       (_sManager.SceneGraph[0] as Player).OnKeyReleased,
                                       (_sManager.SceneGraph[0] as Player).OnNewMouseInput);
                }
            }

            // POPULATE the CollisionManagers collidables List with objects from the Scene Graph:
            _cManager.PopulateCollidables(_sManager.SceneGraph);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // CALL the SceneManagers and CollisionManagers Update method if the program is running:
            if (RUNNING)
            {
                // UPDATE the CollisionManager first:
                _cManager.update();
                // UPDATE the NavigationManager:
                _nManager.Update(gameTime);
                // THEN Update the SceneManager:
                _sManager.Update(gameTime);
                // UPDATE the InputManager:
                _iManager.update();
                // UPDATE the flashlight:
                _flashlight.Update(gameTime);
                // UPDATE the Camera:
                _camera.Update(gameTime);
                // UPDATE base class:
                base.Update(gameTime);
            }
            
        }

        protected override void Draw(GameTime gameTime)
        {
            
            // BEGIN penumbras drawing cycle:
            _penumbra.BeginDraw();
            // SET the window to dark gray:
            GraphicsDevice.Clear(Color.DarkGray);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp,
                               transformMatrix: _camera.Transform);
            // DRAW the Entities that are in the SceneGraph:
            for (int i = 0; i < _sManager.SceneGraph.Count; i++)
            {
                // STOP this loop from drawing the TileMap, as tiles are in the SceneGraph but have their own unique draw method in TileMap:
                if (_sManager.SceneGraph[i] is Tile)
                {
                    // IF it's a Tile, break the loop:
                    break;
                }
                // IF not, draw the GameEntity to the SpriteBatch:
                (_sManager.SceneGraph[i] as GameEntity).Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
