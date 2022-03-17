using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Nosocomephobia.Engine_Code.Entities;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.3, 14-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code
{
    /// <summary>
    /// Enum used to access specific Animations in the animations Dictionary.
    /// </summary>
    public enum AnimationGroup
    {
        // DEFINE Player's Enums for its animations:
        PlayerIdleDown, PlayerIdleUp, PlayerIdleLeft, PlayerIdleRight,
        PlayerWalkDown, PlayerWalkUp, PlayerWalkLeft, PlayerWalkRight,
        PlayerSprintDown, PlayerSprintUp, PlayerSprintLeft, PlayerSprintRight,

        // DEFINE Monster's Enums for its animations:
        MonsterWalkDown, MonsterWalkUp, MonsterWalkLeft, MonsterWalkRight,
    }
    /// <summary>
    /// Static class GameContent. Used to store all of the games images, sounds and font in one contained place.
    /// </summary>
    public static class GameContent
    {
        #region FIELDS
        // DECLARE a static Texture2D, call it _playerSpriteSheet:
        public static Texture2D _playerSpriteSheet;
        // DECLARE a static Texture2D, call it _monsterSpriteSheet:
        public static Texture2D _monsterSpriteSheet;
        // DECLARE a static Texture2D, call it _worldTileSheet:
        public static Texture2D _worldTileSheet;
        public static Texture2D MENU_BACKGROUND;
        // DECLARE a const int, call it DEFAULT_FRAMERATE and set it to 4fps:
        public const int DEFAULT_FRAMERATE = 4;
        // DECLARE a const int, call it DEFAULT_TILE_WIDTH and set it to 32:
        public const int DEFAULT_TILE_WIDTH = 32;
        // DECLARE a const int, call it DEFAULT_TILE_HEIGHT and set it to 32:
        public const int DEFAULT_TILE_HEIGHT = 32;
        // DECLARE a const int, call it TILE_SHEET_WIDTH:
        public const int TILE_SHEET_WIDTH = 800 / DEFAULT_TILE_WIDTH;
        // DECLARE a const int, call it NUMBER_OF_TILES. Represents the total number of individual tiles in the tilesheet:
        public const int NUMBER_OF_TILES = 250;

        public static SoundEffect BackgroundGame;
        public static SoundEffect BackgroundMenu;
        public static SoundEffect DoorUnlock;
        public static SoundEffect Footstep;
        public static SoundEffect Monster1;
        public static SoundEffect Monster2;
        public static SoundEffect Monster3_2;
        public static SoundEffect Price_2;
        public static SoundEffect Price_Distant;
        public static SoundEffect PickupHand;
        public static SoundEffect PickupJournal;
        public static SoundEffect PickupKey;
        public static SoundEffect PickupSaw;

        // DECLARE a static Dictionary to store all of the games animations. Reference each element via the AnimationGroup enum:
        private static Dictionary<AnimationGroup, Animation> animations;
        // DECLARE a stiatc Dictionary to store all Tile Sprites. Reference each one by an int id:
        private static Dictionary<int, Sprite> tileSprites;
        // DECLARE a stiatc Dictionary to store all Artefact Sprites. Reference each one by a string id:
        private static Dictionary<string, Sprite> artefactSprites;
        #endregion

        /// <summary>
        /// Loads all of the games Content. Called from the Kernel.
        /// </summary>
        /// <param name="cm">A Reference to the ContentManager used to load assets.</param>
        public static void LoadContent(ContentManager cm)
        {
            // INITALIZE Fields:
            animations = new Dictionary<AnimationGroup, Animation>();
            tileSprites = new Dictionary<int, Sprite>();
            artefactSprites = new Dictionary<string, Sprite>();

            // LOAD Player's Spritesheet:
            _playerSpriteSheet = cm.Load<Texture2D>("assets/Player_Character/Player_Character_Assets/Nosocomephobia_Player_Character_Sprite_Sheet");
            // LOAD Monster's Spritesheet:
            _monsterSpriteSheet = cm.Load<Texture2D>("assets/Enemy_Character/Final_Assets/Asset_Pack_Monster_Animation");
            // LOAD the World Tile Sheet:
            _worldTileSheet = cm.Load<Texture2D>("worldTilesheet32");

            MENU_BACKGROUND = cm.Load<Texture2D>("assets/UI_Design/Testing/Main_Menu_Backgrounds/Main_Menu_Background022");

            #region PLAYER ANIMATIONS
            // LOAD Player Idle Down:
            LoadAnimation(_playerSpriteSheet, 3, DEFAULT_FRAMERATE, 2, 0, 57, 128, 71, 0, AnimationGroup.PlayerIdleDown);
            // LOAD Player Idle Up:
            LoadAnimation(_playerSpriteSheet, 3, DEFAULT_FRAMERATE, 1, 405, 57, 128, 71, 0, AnimationGroup.PlayerIdleUp);
            // LOAD Player Idle Left:
            LoadAnimation(_playerSpriteSheet, 3, DEFAULT_FRAMERATE, 7, 270, 51, 128, 71, 0, AnimationGroup.PlayerIdleLeft);
            // LOAD Player Idle Right:
            LoadAnimation(_playerSpriteSheet, 3, DEFAULT_FRAMERATE, 3, 135, 51, 128, 71, 0, AnimationGroup.PlayerIdleRight);

            // LOAD Player Walking Down:
            LoadAnimation(_playerSpriteSheet, 4, DEFAULT_FRAMERATE, 5, 540, 55, 128, 71, 0, AnimationGroup.PlayerWalkDown);
            // LOAD Player Walking Up:
            LoadAnimation(_playerSpriteSheet, 4, DEFAULT_FRAMERATE, 2, 675, 55, 128, 71, 0, AnimationGroup.PlayerWalkUp);
            // LOAD Player Walking Left:
            LoadAnimation(_playerSpriteSheet, 4, DEFAULT_FRAMERATE, 4, 945, 57, 128, 71, 0, AnimationGroup.PlayerWalkLeft);
            // LOAD Player Walking Right:
            LoadAnimation(_playerSpriteSheet, 4, DEFAULT_FRAMERATE, 2, 810, 57, 128, 71, 0, AnimationGroup.PlayerWalkRight);

            // LOAD Player Sprinting Down:
            LoadAnimation(_playerSpriteSheet, 6, DEFAULT_FRAMERATE*2, 6, 1080, 53, 128, 71, 0, AnimationGroup.PlayerSprintDown);
            // LOAD Player Sprinting Up:
            LoadAnimation(_playerSpriteSheet, 6, DEFAULT_FRAMERATE*2, 1, 1215, 53, 128, 71, 0, AnimationGroup.PlayerSprintUp);
            // LOAD Player Sprinting Left:
            LoadAnimation(_playerSpriteSheet, 6, DEFAULT_FRAMERATE*2, 0, 1485, 64, 128, 71, 0, AnimationGroup.PlayerSprintLeft);
            // LOAD Player Sprinting Right:
            LoadAnimation(_playerSpriteSheet, 6, DEFAULT_FRAMERATE*2, 0, 1350, 64, 128, 71, 0, AnimationGroup.PlayerSprintRight);
            #endregion

            #region MONSTER ANIMATIONS
            // LOAD Monster Walking Down:
            LoadAnimation(_monsterSpriteSheet, 4, DEFAULT_FRAMERATE, 0, 0, 64, 64, 74, 0, AnimationGroup.MonsterWalkDown);
            // LOAD Monster Walking Up:
            LoadAnimation(_monsterSpriteSheet, 4, DEFAULT_FRAMERATE, 0, 228, 64, 128, 74, 0, AnimationGroup.MonsterWalkUp);
            // LOAD Monster Walking Left:
            LoadAnimation(_monsterSpriteSheet, 4, DEFAULT_FRAMERATE, 0, 76, 128, 64, 131, 0, AnimationGroup.MonsterWalkLeft);
            // LOAD Monster Walking Right:
            LoadAnimation(_monsterSpriteSheet, 4, DEFAULT_FRAMERATE, 0, 152, 128, 64, 131, 0, AnimationGroup.MonsterWalkRight);
            #endregion

            #region LOADING TILESPRITES
            // LOAD the Tile Sprites:
            for (int i = 0; i < NUMBER_OF_TILES; i++)
            {
                ExtractTile(i);
            }
            #endregion

            #region LOADING ARTEFACT SPRITES
            Texture2D journalTexture = cm.Load<Texture2D>("assets/Environmental_Assets/artefact_pngs/journal");
            Texture2D handTexture = cm.Load<Texture2D>("assets/Environmental_Assets/artefact_pngs/hand");
            Texture2D skeletonKeyTexture = cm.Load<Texture2D>("assets/Environmental_Assets/artefact_pngs/skeleton_key");
            Texture2D bonesawTexture = cm.Load<Texture2D>("assets/Environmental_Assets/artefact_pngs/bonesaw");

            Sprite journalSprite = new Sprite(journalTexture, 0, 0, journalTexture.Width, journalTexture.Height);
            Sprite handSprite = new Sprite(handTexture, 0, 0, handTexture.Width, handTexture.Height);
            Sprite skeletonKeySprite = new Sprite(skeletonKeyTexture, 0, 0, skeletonKeyTexture.Width, skeletonKeyTexture.Height);
            Sprite bonesaw = new Sprite(bonesawTexture, 0, 0, bonesawTexture.Width, bonesawTexture.Height);

            artefactSprites.Add("Journal", journalSprite);
            artefactSprites.Add("Hand", handSprite);
            artefactSprites.Add("SkeletonKey", skeletonKeySprite);
            artefactSprites.Add("Bonesaw", bonesaw);
            #endregion

            #region LOADING SOUND EFFECTS
            BackgroundGame = cm.Load<SoundEffect>("assets/audio/background_game");
            BackgroundMenu = cm.Load<SoundEffect>("assets/audio/background_menu");
            DoorUnlock = cm.Load<SoundEffect>("assets/audio/door_unlock");
            Footstep = cm.Load<SoundEffect>("assets/audio/footstep");
            Monster1 = cm.Load<SoundEffect>("assets/audio/monster_1");
            Monster2 = cm.Load<SoundEffect>("assets/audio/monster_2");
            Monster3_2 = cm.Load<SoundEffect>("assets/audio/monster_3.2");
            Price_2 = cm.Load<SoundEffect>("assets/audio/price_2_filtered");
            Price_Distant = cm.Load<SoundEffect>("assets/audio/price_distant");
            PickupHand = cm.Load<SoundEffect>("assets/audio/pickup_hand");
            PickupJournal = cm.Load<SoundEffect>("assets/audio/pickup_journal");
            PickupKey = cm.Load<SoundEffect>("assets/audio/pickup_key");
            PickupSaw = cm.Load<SoundEffect>("assets/audio/pickup_saw");
            #endregion

        }
        /// <summary>
        /// Called from inside this classes constructor. Creates and loads animations.
        /// </summary>
        /// <param name="spriteSheet">The Texture2D SpriteSheet containing the .png file for the animation.</param>
        /// <param name="numberOfFrames">The amount of individual frames in the entityAnimation.</param>
        /// <param name="frameRate">The rate at which the entityAnimation plays in Frames per Second.</param>
        /// <param name="x">The top left pixel origin of the texture on the spritesheet about the X axis.</param>
        /// <param name="y">The top left pixel origin of the texture on the spritesheet about the Y axis.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="xSpacer">The amount of pixels on the X axis between each frame on the spritesheet.</param>
        /// <param name="ySpacer">The amount of pixels on the Y axis between each frame on the spritesheet.</param>
        /// <param name="animationGroup">A reference to the enum storing the names of each entityAnimation. Used when retrieving animations from the Dictionary.</param>
        private static void LoadAnimation(Texture2D spriteSheet, int numberOfFrames, int frameRate, int x, int y, int width, int height, int xSpacer, int ySpacer, AnimationGroup animationGroup)
        {
            // CREATE a new entityAnimation, call it tempAnimation and pass in the frameRate:
            Animation tempAnimation = new Animation(frameRate);

            // LOOP for the total number of frames:
            for (int i = 0; i < numberOfFrames; i++)
            {
                // CREATE a new sprite and call it tempFrame. Pass in the spritesheet and other parameters:
                Sprite tempFrame = new Sprite(spriteSheet, ((i * xSpacer) + x), ((i * ySpacer) + y), width, height);
                // ADD the tempFrame to the tempAnimation:
                tempAnimation.AddFrame(tempFrame);
            }

            // STORE the new tempAnimation in the animations Dictionary, using the name provided from the enum:
            animations.Add(animationGroup, tempAnimation);
        }

        /// <summary>
        /// Gets the Tile image from the tile sheet based on the tileID.
        /// </summary>
        /// <param name="tileID">An int representing the ID number of this Tile.</param>
        private static void ExtractTile(int tileID)
        {
            // DECLARE an int, call it x. Set it to the remainder of tileID by Tile sheet width, * tile width.
            int x = (tileID % TILE_SHEET_WIDTH) * DEFAULT_TILE_WIDTH;
            // DECLARE an int, call it y. Set it to tileID / tile sheet width * tile width.
            int y = (tileID / TILE_SHEET_WIDTH) * DEFAULT_TILE_WIDTH;
            // LOAD the Tiles Sprite:
            LoadTileSprite(x, y, DEFAULT_TILE_WIDTH, DEFAULT_TILE_HEIGHT, tileID);
        }
        /// <summary>
        /// Loads a Tiles Sprite and adds it to the total tileSprites List.
        /// </summary>
        /// <param name="x">The top-left x coordinate that the image originates from in the image file.</param>
        /// <param name="y">The top-left y coordinate that the image originates from in the image file.</param>
        /// <param name="width">The width of the texture in the spritesheet.</param>
        /// <param name="height">The height of the texture in the spritesheet.</param>
        /// <param name="tileID">The Tiles ID number.</param>
        private static void LoadTileSprite(int x, int y, int width, int height, int tileID)
        {
            // CREATE a new Sprite, call it tempTileSprite and pass in the parameters:
            Sprite tempTileSprite = new Sprite(_worldTileSheet, x, y, width, height);
            // ADD the tempTileSprite to the tileSprites, saving them with a tileID:
            tileSprites.Add(tileID, tempTileSprite);
        }

        /// <summary>
        /// Returns a specfic entityAnimation from the collective entityAnimation Dictionary. Uses a name from the enum to locate and return a specific entityAnimation.
        /// </summary>
        /// <param name="animationGroup">The entityAnimation enum tags.</param>
        /// <returns>an Animation from the animation List at the specified index Enum.</returns>
        public static Animation GetAnimation(AnimationGroup animationGroup)
        {
            // RETURN the specific entityAnimation from animations based on the name specificed from the enum:
            return animations[animationGroup];
        }
        /// <summary>
        /// Returns the image Sprite for a Tile based on the ID number.
        /// </summary>
        /// <param name="tileID">The ID Number linked to the Tile image.</param>
        /// <returns>The TileSprite for the Tile matching the ID number.</returns>
        public static Sprite GetTileSprite(int tileID)
        {
            // RETURN tileSprites[tileID]:
            return tileSprites[tileID];
        }
        /// <summary>
        /// Returns the image Sprite for an Artefact based on the string number.
        /// </summary>
        /// <param name="tileID">The ID Number linked to the Tile image.</param>
        /// <returns>The TileSprite for the Tile matching the ID number.</returns>
        public static Sprite GetArtefactSprite(string artefactID)
        {
            // RETURN artefactSprites[artefactID]:
            return artefactSprites[artefactID];
        }
    }
}
