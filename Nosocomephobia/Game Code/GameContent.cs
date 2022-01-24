using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Entities;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.2, 11-12-2021
/// </summary>
namespace Nosocomephobia.Game_Code
{
    /// <summary>
    /// Enum used to access specific Animations in the animations Dictionary.
    /// </summary>
    public enum AnimationGroup
    {
        // DEFINE Player's Enums for its animations:
        PlayerWalkDown, PlayerWalkUp, PlayerWalkLeft, PlayerWalkRight,
        PlayerSprintDown, PlayerSprintUp, PlayerSprintLeft, PlayerSprintRight,
    }
    /// <summary>
    /// Static class GameContent. Used to store all of the games images, sounds and font in one contained place.
    /// </summary>
    public static class GameContent
    {
        #region FIELDS
        // DECLARE a static Texture2D, call it _playerSpriteSheet:
        public static Texture2D _playerSpriteSheet;
        // DECLARE a static Texture2D, call it _worldTileSheet:
        public static Texture2D _worldTileSheet;
        // DECLARE a const int, call it DEFAULT_FRAMERATE and set it to 6fps:
        public const int DEFAULT_FRAMERATE = 6;
        // DECLARE a const int, call it DEFAULT_TILE_WIDTH and set it to 16:
        public const int DEFAULT_TILE_WIDTH = 16;
        // DECLARE a const int, call it DEFAULT_TILE_HEIGHT and set it to 16:
        public const int DEFAULT_TILE_HEIGHT = 16;
        // DECLARE a const int, call it TILE_SHEET_WIDTH:
        public const int TILE_SHEET_WIDTH = 160 / DEFAULT_TILE_WIDTH;
        // DECLARE a const int, call it NUMBER_OF_TILES. Represents the total number of individual tiles in the tilesheet:
        public const int NUMBER_OF_TILES = 40;

        // DECLARE a static Dictionary to store all of the games animations. Reference each element via the AnimationGroup enum:
        private static Dictionary<AnimationGroup, Animation> animations;
        // DECLARE a stiatc Dictionary to store all Tile Sprites. Reference each one by an int id:
        private static Dictionary<int, Sprite> tileSprites;
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

            // LOAD Player's Spritesheet:
            //_playerSpriteSheet = cm.Load<Texture2D>("assets/Player_Character/sprites");
            _playerSpriteSheet = cm.Load<Texture2D>("assets/Enemy_Character/Test_Sprites/Asset_Pack_Monster_Test01");
            // LOAD the World Tile Sheet:
            _worldTileSheet = cm.Load<Texture2D>("tilesheet16");

            #region PLAYER ANIMATIONS
            // LOAD Player Walking Down:
            LoadAnimation(_playerSpriteSheet, 1, DEFAULT_FRAMERATE, 0, 0, 64, 64, 0, 0, AnimationGroup.PlayerWalkDown);
            // LOAD Player Walking Right:
            LoadAnimation(_playerSpriteSheet, 4, DEFAULT_FRAMERATE, 0, 75, 128, 64, 157, 0, AnimationGroup.PlayerWalkLeft);
            // LOAD Player Walking Up:
            LoadAnimation(_playerSpriteSheet, 4, DEFAULT_FRAMERATE, 0, 151, 128, 64, 157, 0, AnimationGroup.PlayerWalkRight);
            // LOAD Player Walking Left:
            LoadAnimation(_playerSpriteSheet, 1, DEFAULT_FRAMERATE, 0, 231, 64, 128, 0, 0, AnimationGroup.PlayerWalkUp);

            // LOAD Player Sprinting Down:
            LoadAnimation(_playerSpriteSheet, 4, DEFAULT_FRAMERATE, 144, 6, 16, 22, 16, 0, AnimationGroup.PlayerSprintDown);
            // LOAD Player Sprinting Right:
            LoadAnimation(_playerSpriteSheet, 4, DEFAULT_FRAMERATE, 146, 38, 14, 22, 16, 0, AnimationGroup.PlayerSprintRight);
            // LOAD Player Sprinting Up:
            LoadAnimation(_playerSpriteSheet, 4, DEFAULT_FRAMERATE, 144, 69, 16, 23, 16, 0, AnimationGroup.PlayerSprintUp);
            // LOAD Player Sprinting Left:
            LoadAnimation(_playerSpriteSheet, 4, DEFAULT_FRAMERATE, 145, 102, 13, 22, 16, 0, AnimationGroup.PlayerSprintLeft);
            #endregion

            #region LOADING TILESPRITES
            // LOAD the Tile Sprites:
            for (int i = 0; i < NUMBER_OF_TILES; i++)
            {
                ExtractTile(i);
            }
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
    }
}
