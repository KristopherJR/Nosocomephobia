using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Components;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.Services;
using Nosocomephobia.Game_Code;
using Nosocomephobia.Game_Code.Game_Entities;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using Nosocomephobia.Game_Code.Screens;
using Nosocomephobia.Game_Code.World;
using Penumbra;
using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 3.5, 24-03-2022
/// 
/// Penumbra Author: Jaanus Varus
/// </summary>

namespace Nosocomephobia
{
    /// <summary>
    /// DECLARE an enum for the games State, which is used to toggle between Screens.
    /// </summary>
    public enum State
    {
        MainMenu, Game, GameOver, Victory
    }
    public class Kernel : Game
    {
        // DECLARE a reference to the State ENUM, call it STATE:
        public static State STATE;
        // DECLARE a public static Boolean, call it 'RUNNING':
        public static Boolean RUNNING;
        // DECLARE a SoundEffectInstance, call it BackgroundMusic:
        public static SoundEffectInstance BackgroundMusic;
        // DECLARE a PenumbraComponent, call it PENUMBRA:
        public static PenumbraComponent PENUMBRA;
        // DECLARE a public static int to represent the Screen Width, call it 'SCREEN_WIDTH':
        public static int SCREEN_WIDTH;
        // DECLARE a public static int to represent the Screen Height, call it 'SCREEN_HEIGHT':
        public static int SCREEN_HEIGHT;
        // DECLARE a static Vector2, call it UP and set it to (0, -1):
        public static Vector2 UP = new Vector2(0, -1);
        // DECLARE a static Vector2, call it DOWN and set it to (0, 1):
        public static Vector2 DOWN = new Vector2(0, 1);
        // DECLARE a static Vector2, call it LEFT and set it to (-1, 0):
        public static Vector2 LEFT = new Vector2(-1, 0);
        // DECLARE a static Vector2, call it RIGHT and set it to (1, 0):
        public static Vector2 RIGHT = new Vector2(1, 0);
        // DECLARE an int, call it SCORE:
        public static int SCORE;

        // DECLARE a const String, call it TILE_MAP_FLOOR_PATH. Set it to the File Path of the floor layer: 
        private const String TILE_MAP_FLOOR_PATH = "Content/nosocomephobia_Floor.csv";
        // DECLARE a const String, call it TILE_MAP_WALLS_PATH. Set it to the File Path of the walls layer:
        private const String TILE_MAP_WALLS_PATH = "Content/nosocomephobia_Walls.csv";
        // DECLARE a const String, call it TILE_MAP_OBJECTS_PATH. Set it to the File Path of the objects layer:
        private const String TILE_MAP_OBJECTS_NON_COLLIDABLE_PATH = "Content/nosocomephobia_Objects-Non-Collidable.csv";
        // DECLARE a const String, call it TILE_MAP_OBJECTS_PATH. Set it to the File Path of the objects layer:
        private const String TILE_MAP_OBJECTS_COLLIDABLE_PATH = "Content/nosocomephobia_Objects-Collidable.csv";

        // DECLARE a bool to toggle between full screen and windowed for development purposes:
        private bool _devMode = false;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        // DECLARE a SpriteBatch for the InventoryHUD to be drawn onto separately, call it _hudSpriteBatch:
        private SpriteBatch _hudSpriteBatch;

        // DECLARE a reference to an IEngineManager, call it _engineManager:
        private IEngineManager _engineManager;

        // DECLARE an IEntityManager, call it 'entityManager':
        private IEntityManager _entityManager;
        // DECLARE an ISceneManager, call it '_sceneManager':
        private ISceneManager _sceneManager;
        // DECLARE an ICollisionManager, call it '_collisionManager':
        private ICollisionManager _collisionManager;
        // DECLARE an IInputManager, call it '_inputManager':
        private IInputManager _inputManager;
        // DECLARE an INavigationManager, call it '_navigationManager':
        private INavigationManager _navigationManager;
        // DECLARE an IObjectPlacementManager, call it '_objectPlacementManager':
        private IObjectPlacementManager _objectPlacementManager;

        // DECLARE an IEntity, call it '_camera':
        private Camera _camera;
        // DECLARE a TileMap, call it '_tileMapFloor':
        private TileMap _tileMapFloor;
        // DECLARE a TileMap, call it '_tileMapWalls':
        private TileMap _tileMapWalls;
        // DECLARE a TileMap, call it '_tileMapObjectsNonCollidable':
        private TileMap _tileMapObjectsNonCollidable;
        // DECLARE a TileMap, call it '_tileMapObjectsnCollidable':
        private TileMap _tileMapObjectsCollidable;
        // DECLARE a HullMap, call it '_wallHullMap':
        private HullMap _wallHullMap;
        // DECLARE a Dictionary<string, Screen>, call it _screens:
        private Dictionary<string, Screen> _screens;

        /// <summary>
        /// Constructor for Kernel.
        /// </summary>
        /// <param name="pEngineManager">A reference to the Engine Manager.</param>
        public Kernel(IEngineManager pEngineManager)
        {
            // INTIALISE the EngineManager:
            _engineManager = pEngineManager;
            // SET start-up values:
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            STATE = State.MainMenu;
            _screens = new Dictionary<string, Screen>();
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
            // SET the AmbientColor of Penumbra so that the scene is dark (10% light):
            PENUMBRA.AmbientColor = new Color(new Vector3(0.1f));
            // ADD penumbra to game components:
            Components.Add(PENUMBRA);
            // CALL penumbras intialize method:
            PENUMBRA.Initialize();
            // INITALISE the base class:
            base.Initialize();
            // CREATE a new Screen for the Main Menu:
            Screen mainMenuScreen = new MainMenuScreen();
            // CREATE a new Screen for Game Over:
            Screen gameOverScreen = new GameOverScreen();
            // CREATE a new Screen for Game Over:
            Screen victoryScreen = new VictoryScreen();
            // ADD the new Screens to _screens:
            _screens.Add("Main_Menu", mainMenuScreen);
            _screens.Add("GameOver", gameOverScreen);
            _screens.Add("Victory", victoryScreen);

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
            // INITALISE the SpriteBatches:
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _hudSpriteBatch = new SpriteBatch(GraphicsDevice);
            // LOADING the game content:
            GameContent.LoadContent(Content);
            // SET the game background music:
            BackgroundMusic = GameContent.BackgroundMenu.CreateInstance();
            BackgroundMusic.IsLooped = true;
            BackgroundMusic.Volume = 0.6f;
            BackgroundMusic.Play();
            // INITALIZE tilemaps:
            _tileMapFloor = new TileMap(TILE_MAP_FLOOR_PATH, false);
            _tileMapWalls = new TileMap(TILE_MAP_WALLS_PATH, true);
            _tileMapObjectsNonCollidable = new TileMap(TILE_MAP_OBJECTS_NON_COLLIDABLE_PATH, false);
            _tileMapObjectsCollidable = new TileMap(TILE_MAP_OBJECTS_COLLIDABLE_PATH, true);
            _objectPlacementManager = new ObjectPlacementManager(_tileMapFloor);
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
            _sceneManager.SceneGraphs["GameScene"].CreateLayer("TileMapObjectsNonCollidable", 3);
            _sceneManager.SceneGraphs["GameScene"].CreateLayer("TileMapObjectsCollidable", 4);
            _sceneManager.SceneGraphs["GameScene"].CreateLayer("Artefacts", 5);
            _sceneManager.SceneGraphs["GameScene"].CreateLayer("Entities", 6);
        }

        private void SpawnGameEntities()
        {
            #region PLAYER
            // REQUEST a new 'Player' object from the EntityManager (uses EntityFactory to create) and call it player:
            IEntity player = _entityManager.CreateEntity<Player>();
            // CREATE the Players Flashlight using the EntityManager Factory:
            (player as Player).Flashlight = _entityManager.CreateEntity<Flashlight>() as Flashlight;
            // INITIALISE the flashlight:
            (player as Player).Flashlight.Initialise((_camera as Camera));
            // ADD the flashlight to the penumbra engine:
            PENUMBRA.Lights.Add((player as Player).Flashlight.Light);

            // SPAWN _player into the 'GameSceneGraph' on the 'Entities' layer:
            _sceneManager.Spawn("GameScene", "Entities", player);

            // SET _camera focus onto Player:
            (_camera as Camera).SetFocus(player as GameEntity);
            // SET _flashlight focus onto Player:
            (player as Player).Flashlight.SetFocus(player as GameEntity);

            // CREATE a new Screen for the Game:
            Screen gameScreen = new GameScreen(_engineManager, _sceneManager, _camera, player as Player);
            _screens.Add("Game", gameScreen);
            #endregion PLAYER

            #region MONSTER
            // REQUEST a new 'NPC' object from the EntityManager (uses EntityFactory to create) and call it monster:
            IEntity monster = _entityManager.CreateEntity<Monster>();
            // SPAWN monster into the 'GameSceneGraph' on the 'Entities' layer:
            _sceneManager.Spawn("GameScene", "Entities", monster);
            // SET the monsters Navigation Gris to the collision map:
            _navigationManager.NavigationGrid = _tileMapWalls;
            // SET the monsters target to the player:
            _navigationManager.Target = player as GameEntity;
            // ADD the monster as a path finder to the Navigation Manager:
            _navigationManager.AddPathFinder(monster as IPathFinder);
            #endregion MONSTER

            #region HULLS
            // INITIALISE the HullMap and pass in the collision TileMap as a parameter:
            _wallHullMap = new HullMap(_tileMapWalls);
            // ITERATE throught the HullMap:
            foreach (Hull h in _wallHullMap.GetHulls())
            {
                // ADD each Hull to the Penumbra Component:
                PENUMBRA.Hulls.Add(h); // works fine, adds all 6,210 wall tiles as hulls
            }
            #endregion HULLS

            #region TILES
            // FOREACH Tile in TileMap floor Layer:
            foreach (Tile t in _tileMapFloor.GetTileMap())
            {
                // TEST that the tile is valid before spawning it to the scene graph:
                if (t.IsValidTile)
                {
                    // SPAWN the Tiles into the SceneGraph:
                    _sceneManager.Spawn("GameScene", "TileMapFloor", t);
                }
            }

            // FOREACH Tile in TileMap collision Layer:
            foreach (Tile t in _tileMapWalls.GetTileMap())
            {
                // TEST that the tile is valid before spawning it to the scene graph:
                if (t.IsValidTile)
                {
                    // SPAWN the Tiles into the SceneGraph:
                    _sceneManager.Spawn("GameScene", "TileMapWalls", t);
                }
            }

            // FOREACH Tile in TileMap objects non-collidable Layer:
            foreach (Tile t in _tileMapObjectsNonCollidable.GetTileMap())
            {
                // TEST that the tile is valid before spawning it to the scene graph:
                if (t.IsValidTile)
                {
                    // SPAWN the Tiles into the SceneGraph:
                    _sceneManager.Spawn("GameScene", "TileMapObjectsNonCollidable", t);
                }
            }

            // FOREACH Tile in TileMap objects collidable Layer:
            foreach (Tile t in _tileMapObjectsCollidable.GetTileMap())
            {
                // TEST that the tile is valid before spawning it to the scene graph:
                if (t.IsValidTile)
                {
                    // SPAWN the Tiles into the SceneGraph:
                    _sceneManager.Spawn("GameScene", "TileMapObjectsCollidable", t);
                }
            }
            #endregion TILES

            #region ARTEFACTS
            // CREATE Artefacts with Entity Factory:
            IEntity journalArtefact = _entityManager.CreateEntity<Artefact>();
            IEntity bonesawArtefact = _entityManager.CreateEntity<Artefact>();
            IEntity skeletonKeyArtefact = _entityManager.CreateEntity<Artefact>();
            IEntity handArtefact = _entityManager.CreateEntity<Artefact>();
            IEntity door = _entityManager.CreateEntity<Door>();

            // SET their sprites:
            (journalArtefact as GameEntity).EntitySprite = GameContent.GetArtefactSprite("Journal");
            (bonesawArtefact as GameEntity).EntitySprite = GameContent.GetArtefactSprite("Bonesaw");
            (skeletonKeyArtefact as GameEntity).EntitySprite = GameContent.GetArtefactSprite("SkeletonKey");
            (handArtefact as GameEntity).EntitySprite = GameContent.GetArtefactSprite("Hand");

            // SET their SFXs:
            (journalArtefact as Artefact).PickupSFX = GameContent.PickupJournal;
            (bonesawArtefact as Artefact).PickupSFX = GameContent.PickupSaw;
            (skeletonKeyArtefact as Artefact).PickupSFX = GameContent.PickupKey;
            (handArtefact as Artefact).PickupSFX = GameContent.PickupHand;

            // ADD them to the ObjectPlacementManager:
            _objectPlacementManager.Artefacts.Add(journalArtefact as Artefact);
            _objectPlacementManager.Artefacts.Add(bonesawArtefact as Artefact);
            _objectPlacementManager.Artefacts.Add(skeletonKeyArtefact as Artefact);
            _objectPlacementManager.Artefacts.Add(handArtefact as Artefact);

            // SET their locations:
            _objectPlacementManager.RandomiseArtefactPlacements();
            (door as GameEntity).EntityLocn = new Vector2(2976, 5665);

            // SPAWN them onto the Artefacts Layer in the Game SceneGraph:
            _sceneManager.Spawn("GameScene", "Artefacts", journalArtefact);
            _sceneManager.Spawn("GameScene", "Artefacts", handArtefact);
            _sceneManager.Spawn("GameScene", "Artefacts", skeletonKeyArtefact);
            _sceneManager.Spawn("GameScene", "Artefacts", bonesawArtefact);
            _sceneManager.Spawn("GameScene", "Artefacts", door);
            #endregion ARTEFACTS

            // SUBSCRIBE entities on the active scene graph to Input events:
            _sceneManager.RefreshInputEvents();
            // SUBSCRIBE entities on the active scene graph to Collision events:
            _sceneManager.RefreshCollisionEvents();
        }

        /// <summary>
        /// Main Update method for Kernel. Changes based on State.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!RUNNING)
            {
                // EXIT the game if RUNNING is false:
                this.Exit();
            }
            // CHECK the program State and Update the appropriate screen for the corresponding State:
            switch (STATE)
            {
                case State.MainMenu:
                    UpdateMainMenu(gameTime);
                    break;
                case State.Game:
                    UpdateGame(gameTime);
                    base.Update(gameTime);
                    break;
                case State.GameOver:
                    UpdateGameOver(gameTime);
                    break;
                case State.Victory:
                    UpdateVictory(gameTime);
                    break;
            }

        }
        /// <summary>
        /// Main Draw method for Kernel. Changes based on State.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        protected override void Draw(GameTime gameTime)
        {
            // CHECK the program State and Draw the appropriate screen for the corresponding State:
            switch (STATE)
            {
                case State.MainMenu:
                    DrawMainMenu(gameTime);
                    break;
                case State.Game:
                    DrawGame(gameTime);
                    break;
                case State.GameOver:
                    DrawGameOver(gameTime);
                    break;
                case State.Victory:
                    DrawVictory(gameTime);
                    break;
            }

        }
        #region UPDATES
        /// <summary>
        /// Updates the Main Menu Screen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        private void UpdateMainMenu(GameTime gameTime)
        {
            // UPDATE the Main Menu Screen:
            _screens["Main_Menu"].Update(gameTime);

        }
        /// <summary>
        /// Updates the Game Screen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        private void UpdateGame(GameTime gameTime)
        {
            // UPDATE the Game Screen:
            _screens["Game"].Update(gameTime);
        }
        /// <summary>
        /// Updates the GameOver Screen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        private void UpdateGameOver(GameTime gameTime)
        {
            // UPDATE the GameOver Screen:
            _screens["GameOver"].Update(gameTime);
        }
        /// <summary>
        /// Updates the Victory Screen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        private void UpdateVictory(GameTime gameTime)
        {
            // UPDATE the Victory Screen:
            _screens["Victory"].Update(gameTime);
        }
        #endregion

        #region DRAWS
        /// <summary>
        /// Draws the Main Menu Screen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        private void DrawMainMenu(GameTime gameTime)
        {
            // DRAW the Main Menu Screen:
            _screens["Main_Menu"].Draw(gameTime, _spriteBatch, this.GraphicsDevice);
        }
        /// <summary>
        /// Draws the Game Screen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        private void DrawGame(GameTime gameTime)
        {
            // DRAW the Game Screen:
            _screens["Game"].Draw(gameTime, _spriteBatch, this.GraphicsDevice);
            // APPLY the Penumbra changes:
            base.Draw(gameTime);
            // DRAW the InventoryHUD AFTER so that the Penumbra lighting does not affect the HUD:
            (_screens["Game"] as GameScreen).InventoryHUD.Draw(gameTime, _hudSpriteBatch);
        }
        /// <summary>
        /// Draws the Game Over Screen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        private void DrawGameOver(GameTime gameTime)
        {
            // DRAW the GameOver Screen:
            _screens["GameOver"].Draw(gameTime, _spriteBatch, this.GraphicsDevice);
        }
        /// <summary>
        /// Draws the Victory Screen.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        private void DrawVictory(GameTime gameTime)
        {
            // DRAW the Victory Screen:
            _screens["Victory"].Draw(gameTime, _spriteBatch, this.GraphicsDevice);
        }
        #endregion
    }
}
