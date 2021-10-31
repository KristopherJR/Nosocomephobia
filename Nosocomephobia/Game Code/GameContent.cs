﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Entities;
using System.Collections.Generic;

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
        // DECLARE a static Texture2D, call it PlayerSpriteSheet:
        public static Texture2D PlayerSpriteSheet;
        // DECLARE a static Dictionary to store all of the games animations. Reference each element via the AnimationGroup enum:
        private static Dictionary<AnimationGroup, Animation> animations;
        // DECLARE a stiatc Dictionary to store all Tile Sprites. Reference each one by an int id:
        private static Dictionary<int, Sprite> tileSprites;
        // DECLARE a const int, call it DEFAULT_FRAMERATE and set it to 6fps:
        public const int DEFAULT_FRAMERATE = 6;
        // DECLARE a const int, call it DEFAULT_TILE_WIDTH and set it to 16:
        public const int DEFAULT_TILE_WIDTH = 16;
        // DECLARE a const int, call it DEFAULT_TILE_HEIGHT and set it to 16:
        public const int DEFAULT_TILE_HEIGHT = 16;
        // DECLARE a const int, call it TILE_SHEET_WIDTH:
        public const int TILE_SHEET_WIDTH = 352 / DEFAULT_TILE_WIDTH;
        // DECLARE a const int, call it NUMBER_OF_TILES:
        public const int NUMBER_OF_TILES = 397;
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
            PlayerSpriteSheet = cm.Load<Texture2D>("sam_spritesheet");

            #region SAM ANIMATIONS
            // LOAD Player Walking Down:
            LoadAnimation(PlayerSpriteSheet, 4, DEFAULT_FRAMERATE, 1, 6, 15, 22, 16, 0, AnimationGroup.PlayerWalkDown);
            // LOAD Player Walking Right:
            LoadAnimation(PlayerSpriteSheet, 4, DEFAULT_FRAMERATE, 2, 38, 13, 22, 16, 0, AnimationGroup.PlayerWalkRight);
            // LOAD Player Walking Up:
            LoadAnimation(PlayerSpriteSheet, 4, DEFAULT_FRAMERATE, 0, 69, 15, 23, 16, 0, AnimationGroup.PlayerWalkUp);
            // LOAD Player Walking Left:
            LoadAnimation(PlayerSpriteSheet, 4, DEFAULT_FRAMERATE, 1, 102, 13, 22, 16, 0, AnimationGroup.PlayerWalkLeft);

            // LOAD Player Sprinting Down:
            LoadAnimation(PlayerSpriteSheet, 4, DEFAULT_FRAMERATE, 144, 6, 16, 22, 16, 0, AnimationGroup.PlayerSprintDown);
            // LOAD Player Sprinting Right:
            LoadAnimation(PlayerSpriteSheet, 4, DEFAULT_FRAMERATE, 146, 38, 14, 22, 16, 0, AnimationGroup.PlayerSprintRight);
            // LOAD Player Sprinting Up:
            LoadAnimation(PlayerSpriteSheet, 4, DEFAULT_FRAMERATE, 144, 69, 16, 23, 16, 0, AnimationGroup.PlayerSprintUp);
            // LOAD Player Sprinting Left:
            LoadAnimation(PlayerSpriteSheet, 4, DEFAULT_FRAMERATE, 145, 102, 13, 22, 16, 0, AnimationGroup.PlayerSprintLeft);
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
