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
/// Version: 0.5, 31-01-2022
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

        // DECLARE a const String, call it TILE_MAP_FLOOR_PATH. Set it to the File Path of the floor layer: 
        private const String TILE_MAP_FLOOR_PATH = "Content/3x-floors.csv";
        // DECLARE a const String, call it TILE_MAP_COLLISION_PATH. Set it to the File Path of the collision layer:
        private const String TILE_MAP_COLLISION_PATH = "Content/3x-walls.csv";

        // DECLARE a bool to toggle between full screen and windowed for development purposes:
        private bool _devMode = true;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private IEngineManager _engineManager;

        // DECLARE an EntityManager, call it 'entityManager'. Store it as its interface IEntityManager:
        private IEntityManager _entityManager;
        // DECLARE a SceneManager, call it '_sceneManager'. Store it as its interface ISceneManager:
        private ISceneManager _sceneManager;
        // DECLARE a CollisionManager, call it '_collisionManager'. Store it as its interface ICollisionManager:
        private ICollisionManager _collisionManager;
        // DECLARE an InputManager, call it '_inputManager'. Store it as its interface IInputManager:
        private IInputManager _inputManager;
        // DECLARE an NavigationManager, call it '_navigationManager'. Store it as its interface INavigationManager:
        private INavigationManager _navigationManager;

        // DECLARE a Camera, call it '_camera':
        private Camera _camera;
        // DECLARE a TileMap, call it '_tileMapFloor':
        private TileMap _tileMapFloor;
        // DECLARE a TileMap, call it '_tileMapCollisions':
        private TileMap _tileMapCollisions;

        // DECLARE a PenumbraComponent, call it _penumbra:
        private PenumbraComponent _penumbra;

        // DECLARE an Flashlight, call it _flashlight:
        private Flashlight _flashlight;
        
        public Kernel(IEngineManager pEngineManager)
        {
            // INTIALISE the EngineManager:
            _engineManager = pEngineManager;

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

            // INITIALISE the EngineManagers Services:
            _engineManager.InitialiseServices();

            // STORE a copy reference of the EngineManagers services for use in the Kernel:

            _entityManager = (_engineManager.GetService<IEntityManager>() as IEntityManager);
            _sceneManager = (_engineManager.GetService<ISceneManager>() as ISceneManager);
            _collisionManager = (_engineManager.GetService<ICollisionManager>() as ICollisionManager);
            _inputManager = (_engineManager.GetService<IInputManager>() as IInputManager);
            _navigationManager = (_engineManager.GetService<INavigationManager>() as INavigationManager);

            // INJECT the _inputManager and _collisionManager into the _sceneManager for use with handling SceneGraphs:
            _sceneManager.InjectInputManager(_inputManager);
            _sceneManager.InjectCollisionManager(_collisionManager);


            // INITIALIZE the camera:
            _camera = new Camera(GraphicsDevice.Viewport);

            // SUBSCRIBE the camera to listen for input events:
            _inputManager.Subscribe(_camera,
                                    _camera.OnNewInput,
                                    _camera.OnKeyReleased,
                                    _camera.OnNewMouseInput);

            // INITIALISE the flashlight:
            _flashlight.Initialise(_camera);
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
        /// SpawnGameEntities is used to spawn all game objects into the SceneGraph/EntityPool/CollisionMap. Called in Kernel from LoadContent().
        /// </summary>


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // LOADING the game content:
            GameContent.LoadContent(Content);
            // INITALIZE tilemaps:
            _tileMapFloor = new TileMap(TILE_MAP_FLOOR_PATH, false);
            _tileMapCollisions = new TileMap(TILE_MAP_COLLISION_PATH, true);
            // CREATE the games SceneGraphs:
            this.CreateSceneGraphs();
            // SPAWN the game objects:
            this.SpawnGameEntities();
        }

        /// <summary>
        /// Sets up all of the SceneGraphs in the Game. I.E MenuScene, GameScene.
        /// This is also responsible for specifying the Layers within each SceneGraph.
        /// </summary>
        private void CreateSceneGraphs()
        {
            // CREATE GameScene:
            _sceneManager.CreateSceneGraph("GameScene", true);
            // CREATE Layers for GameScene:
            _sceneManager.SceneGraphs["GameScene"].CreateLayer("TileMapFloor", 1);
            _sceneManager.SceneGraphs["GameScene"].CreateLayer("TileMapWalls", 2);
            _sceneManager.SceneGraphs["GameScene"].CreateLayer("Entities", 3);
        }

        private void SpawnGameEntities()
        {
            // REQUEST a new 'Player' object from the EntityManager, and pass it to the SceneManager. Call it _player.:
            IEntity _player = _entityManager.createEntity<Player>();
            // SPAWN _player into the SceneGraph:
            _sceneManager.Spawn("GameScene", "Entities", _player);
            // SET _camera focus onto Player:
            _camera.SetFocus(_player as GameEntity);
            // SET _flashlight focus onto Player:
            _flashlight.SetFocus(_player as GameEntity);

            // FOREACH Tile in TileMap floor Layer:
            foreach (Tile t in _tileMapFloor.GetTileMap())
            {
                // TEST that the tile is valid before spawning it to the scene graph:
                if(t.IsValidTile)
                {
                    // SPAWN the Tiles into the SceneGraph:
                    _sceneManager.Spawn("GameScene", "TileMapFloor", t);
                } 
            }

            // FOREACH Tile in TileMap collision Layer:
            foreach (Tile t in _tileMapCollisions.GetTileMap())
            {
                // TEST that the tile is valid before spawning it to the scene graph:
                if (t.IsValidTile)
                {
                    // SPAWN the Tiles into the SceneGraph:
                    _sceneManager.Spawn("GameScene", "TileMapWalls", t);
                }
            }
            // SUSCRIBE entities on the active scene graph to Input events:
            _sceneManager.UpdateInputEvents();
            // SUBSCRIBE entities on the active scene graph to Collision events:
            _sceneManager.UpdateCollisionEvents();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // CALL the SceneManagers and CollisionManagers Update method if the program is running:
            if (RUNNING)
            {
                // UPDATE the EngineManager:
                _engineManager.Update(gameTime);
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
            // SET the transform of the Penumbra engine to the Cameras Transform:
            _penumbra.Transform = _camera.Transform;
            // BEGIN penumbras drawing cycle:
            _penumbra.BeginDraw();
            // SET the window to dark gray:
            GraphicsDevice.Clear(Color.DarkGray);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp,
                               transformMatrix: _camera.Transform);
            _sceneManager.DrawSceneGraphs(_spriteBatch);
            
    
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
