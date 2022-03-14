using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Components;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.Services;
using Nosocomephobia.Game_Code;
using Nosocomephobia.Game_Code.Game_Entities;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using Nosocomephobia.Game_Code.World;
using Penumbra;
using System;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 2.1, 14-03-2022
/// 
/// Penumbra Author: Jaanus Varus
/// </summary>

namespace Nosocomephobia
{
    public class Kernel : Game
    {
        // DECLARE a public static Boolean, call it 'RUNNING':
        public static Boolean RUNNING;
        // DECLARE a PenumbraComponent, call it PENUMBRA:
        public static PenumbraComponent PENUMBRA;
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

        // DECLARE a reference to an IEngineManager, call it _engineManager:
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

        // DECLARE an IEntity, call it '_camera':
        private Camera _camera;
        // DECLARE a TileMap, call it '_tileMapFloor':
        private TileMap _tileMapFloor;
        // DECLARE a TileMap, call it '_tileMapCollisions':
        private TileMap _tileMapCollisions;

        /// <summary>
        /// Constructor for Kernel.
        /// </summary>
        /// <param name="pEngineManager">A reference to the Engine Manager.</param>
        public Kernel(IEngineManager pEngineManager)
        {
            // INTIALISE the EngineManager:
            _engineManager = pEngineManager;

            _graphics = new GraphicsDeviceManager(this);
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
            // INITIALIZE the Camera:
            _camera = _entityManager.CreateEntity<Camera>() as Camera;
            (_camera as Camera).InjectViewPort(_graphics.GraphicsDevice.Viewport);
            

            // SUBSCRIBE the camera to listen for input events:
            _inputManager.Subscribe((_camera as Camera),
                                    (_camera as Camera).OnNewInput,
                                    (_camera as Camera).OnKeyReleased,
                                    (_camera as Camera).OnNewMouseInput);

            
            

            
            // INTIALISE penumbra as a PenumbraComponent:
            PENUMBRA = new PenumbraComponent(this);
            
            // ADD penumbra to game components:
            Components.Add(PENUMBRA);
            // CALL penumbras intialize method:
            PENUMBRA.Initialize();
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
            #region PLAYER
            // REQUEST a new 'Player' object from the EntityManager (uses EntityFactory to create) and call it _player:
            IEntity _player = _entityManager.CreateEntity<Player>();
            // CREATE the Players Flashlight using the EntityManager Factory:
            (_player as Player).Flashlight = _entityManager.CreateEntity<Flashlight>() as Flashlight;
            // INITIALISE the flashlight:
            (_player as Player).Flashlight.Initialise((_camera as Camera));
            // ADD the flashlight to the penumbra engine:
            PENUMBRA.Lights.Add((_player as Player).Flashlight.Light);

            // SPAWN _player into the 'GameSceneGraph' on the 'Entities' layer:
            _sceneManager.Spawn("GameScene", "Entities", _player);

            // SET _camera focus onto Player:
            (_camera as Camera).SetFocus(_player as GameEntity);
            // SET _flashlight focus onto Player:
            (_player as Player).Flashlight.SetFocus(_player as GameEntity);
            #endregion PLAYER

            #region MONSTER
            // REQUEST a new 'NPC' object from the EntityManager (uses EntityFactory to create) and call it _monster:
            IEntity _monster = _entityManager.CreateEntity<NPC>();
            // SPAWN _monster into the 'GameSceneGraph' on the 'Entities' layer:
            _sceneManager.Spawn("GameScene", "Entities", _monster);
            _navigationManager.NavigationGrid = _tileMapCollisions;
            _navigationManager.Target = _player as GameEntity;
            _navigationManager.AddPathFinder(_monster as IPathFinder);
            #endregion MONSTER

            #region TILES
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
            #endregion TILES

            // SUBSCRIBE entities on the active scene graph to Input events:
            _sceneManager.RefreshInputEvents();
            // SUBSCRIBE entities on the active scene graph to Collision events:
            _sceneManager.RefreshCollisionEvents();
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
                // UPDATE the Camera:
                _camera.Update(gameTime);
                // UPDATE base class:
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            // SET the transform of the Penumbra engine to the Cameras Transform:
            PENUMBRA.Transform = (_camera as Camera).Transform;
            // BEGIN penumbras drawing cycle:
            PENUMBRA.BeginDraw();
            // SET the window to dark gray:
            GraphicsDevice.Clear(Color.DarkGray);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp,
                               transformMatrix: (_camera as Camera).Transform);

            _sceneManager.DrawSceneGraphs(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
