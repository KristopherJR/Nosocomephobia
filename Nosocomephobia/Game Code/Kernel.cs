﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
/// Version: 3.0, 17-03-2022
/// 
/// Penumbra Author: Jaanus Varus
/// </summary>

namespace Nosocomephobia
{
    public enum State
    {
        MainMenu, Game, Pause, GameOver
    }
    public class Kernel : Game
    {
        public static State STATE;
        // DECLARE a public static Boolean, call it 'RUNNING':
        public static Boolean RUNNING;
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

        private HullMap _wallHullMap;

        private Dictionary<string, Screen> _screens;

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
            PENUMBRA.AmbientColor = new Color(new Vector3(0.1f));
            // ADD penumbra to game components:
            Components.Add(PENUMBRA);
            // CALL penumbras intialize method:
            PENUMBRA.Initialize();
            // INITALISE the base class:
            base.Initialize();
            Screen mainMenuScreen = new MainMenuScreen();
            Screen gameScreen = new GameScreen(_engineManager, _sceneManager, _camera);

            _screens.Add("Main_Menu", mainMenuScreen);
            _screens.Add("Game", gameScreen);
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
            // SET the game background music:
            SoundEffectInstance backgroundMusic = GameContent.BackgroundGame.CreateInstance();
            backgroundMusic.IsLooped = true;
            backgroundMusic.Volume = 0.3f;
            backgroundMusic.Play();
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
            _sceneManager.SceneGraphs["GameScene"].CreateLayer("Artefacts", 3);
            _sceneManager.SceneGraphs["GameScene"].CreateLayer("Entities", 4);
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
            #endregion PLAYER

            

            #region MONSTER
            // REQUEST a new 'NPC' object from the EntityManager (uses EntityFactory to create) and call it monster:
            IEntity monster = _entityManager.CreateEntity<Monster>();
            // SPAWN _monster into the 'GameSceneGraph' on the 'Entities' layer:
            _sceneManager.Spawn("GameScene", "Entities", monster);
            _navigationManager.NavigationGrid = _tileMapCollisions;
            _navigationManager.Target = player as GameEntity;
            _navigationManager.AddPathFinder(monster as IPathFinder);
            #endregion MONSTER

            #region HULLS
            _wallHullMap = new HullMap(_tileMapCollisions);
            foreach (Hull h in _wallHullMap.GetHulls())
            {
                PENUMBRA.Hulls.Add(h); // works fine, adds all 6,210 wall tiles as hulls
            }
            #endregion HULLS

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

            #region ARTEFACTS
            // CREATE Artefacts with Entity Factory:
            IEntity journalArtefact = _entityManager.CreateEntity<Artefact>();
            IEntity handArtefact = _entityManager.CreateEntity<Artefact>();
            IEntity skeletonKeyArtefact = _entityManager.CreateEntity<Artefact>();
            IEntity bonesawArtefact = _entityManager.CreateEntity<Artefact>();

            // SET their sprites:
            (journalArtefact as GameEntity).EntitySprite = GameContent.GetArtefactSprite("Journal");
            (handArtefact as GameEntity).EntitySprite = GameContent.GetArtefactSprite("Hand");
            (skeletonKeyArtefact as GameEntity).EntitySprite = GameContent.GetArtefactSprite("SkeletonKey");
            (bonesawArtefact as GameEntity).EntitySprite = GameContent.GetArtefactSprite("Bonesaw");

            // SET their SFXs:
            (journalArtefact as Artefact).PickupSFX = GameContent.PickupJournal;
            (handArtefact as Artefact).PickupSFX = GameContent.PickupHand;
            (skeletonKeyArtefact as Artefact).PickupSFX = GameContent.PickupKey;
            (bonesawArtefact as Artefact).PickupSFX = GameContent.PickupSaw;

            // SET their locations:
            (journalArtefact as GameEntity).EntityLocn = new Vector2(200, 7175);
            (handArtefact as GameEntity).EntityLocn = new Vector2(1780, 4275);
            (skeletonKeyArtefact as GameEntity).EntityLocn = new Vector2(5710, 3485);
            (bonesawArtefact as GameEntity).EntityLocn = new Vector2(4770, 6905);

            // SPAWN them onto the Artefacts Layer in the Game SceneGraph:
            _sceneManager.Spawn("GameScene", "Artefacts", journalArtefact);
            _sceneManager.Spawn("GameScene", "Artefacts", handArtefact);
            _sceneManager.Spawn("GameScene", "Artefacts", skeletonKeyArtefact);
            _sceneManager.Spawn("GameScene", "Artefacts", bonesawArtefact);
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


            switch (STATE)
            {
                case State.MainMenu:
                    UpdateMainMenu(gameTime);
                    break;
                case State.Game:
                    UpdateGame(gameTime);
                    break;
                case State.Pause:
                    UpdatePause(gameTime);
                    break;
                case State.GameOver:
                    UpdateGameOver(gameTime);
                    break;
            }  
            base.Update(gameTime);
        }
        /// <summary>
        /// Main Draw method for Kernel. Changes based on State.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            switch (STATE)
            {
                case State.MainMenu:
                    DrawMainMenu(gameTime);
                    break;
                case State.Game:
                    DrawGame(gameTime);
                    break;
                case State.Pause:
                    DrawPause(gameTime);
                    break;
                case State.GameOver:
                    DrawGameOver(gameTime);
                    break;
            }

        }
        #region UPDATES
        private void UpdateMainMenu(GameTime gameTime)
        {
            _screens["Main_Menu"].Update(gameTime);

        }
        private void UpdateGame(GameTime gameTime)
        {
            // CALL the SceneManagers and CollisionManagers Update method if the program is running:
            if (RUNNING)
            {
                _screens["Game"].Update(gameTime);
            }
        }
        private void UpdatePause(GameTime gameTime)
        {
            
        }
        private void UpdateGameOver(GameTime gameTime)
        {

        }
        #endregion

        #region DRAWS
        private void DrawMainMenu(GameTime gameTime)
        {
            _screens["Main_Menu"].Draw(gameTime, _spriteBatch, this.GraphicsDevice);
        }
        private void DrawGame(GameTime gameTime)
        {

            _screens["Game"].Draw(gameTime, _spriteBatch, this.GraphicsDevice);
            base.Draw(gameTime);
        }
        private void DrawPause(GameTime gameTime)
        {

        }
        private void DrawGameOver(GameTime gameTime)
        {

        }
        #endregion
    }
}
