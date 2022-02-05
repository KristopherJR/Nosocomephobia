﻿using Microsoft.Xna.Framework;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.1, 11-12-2021
/// </summary>
namespace Nosocomephobia.Game_Code.World
{
    public class Tile : GameEntity, ICollidable
    {
        #region FIELDS
        // DECLARE a bool, call it _isValidTile:
        private bool _isValidTile;
        #endregion

        #region PROPERTIES
        // DECLARE a get-set property for _isValidTile:
        public bool IsValidTile
        {
            get { return _isValidTile; }
            set { _isValidTile = value; }
        }
        #endregion

        /// <summary>
        /// Constructor for objects of class Tile.
        /// </summary>
        /// <param name="tileID">An int specifying the new Tiles ID number.</param>
        public Tile(int tileID) : base()
        {
            // INITIALIZE fields:
            this.entitySprite = this.LoadTileSprite(tileID);
            this.entityLocn = new Vector2(0, 0);
            // MAKE all tiles valid by default:
            _isValidTile = true;
        }

        /// <summary>
        /// SETS the position of the Tile in the Game World:
        /// </summary>
        /// <param name="tilePosition">A Vector2 containing the position to set the Tile at.</param>
        public void SetTilePosition(Vector2 tilePosition)
        {
            // SET this Tiles location to the passed in parameter:
            this.entityLocn = new Vector2(tilePosition.X, tilePosition.Y);
        }

        /// <summary>
        /// LOADS a graphical Sprite for this Tile.
        /// </summary>
        /// <param name="tileID">An int specifying the new Tiles ID number.</param>
        /// <returns></returns>
        private Sprite LoadTileSprite(int tileID)
        {
            // DECLARE a new Sprite, call it retrievedTile. Set it to the return of GameContent.GetTileSprite(tileID):
            Sprite retrievedTile = GameContent.GetTileSprite(tileID);
            // RETURN the retrievedTile:
            return retrievedTile;
        }
    }
}
