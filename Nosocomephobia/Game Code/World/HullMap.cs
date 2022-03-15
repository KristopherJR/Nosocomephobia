using Microsoft.Xna.Framework;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.1, 15-03-2021
/// </summary>
namespace Nosocomephobia.Game_Code.World
{
    /// <summary>
    /// Class HullMap
    /// </summary>
    public class HullMap
    {
        #region FIELDS
        // DECLARE a List<Hull>, call it _hulls:
        private List<Hull> _hulls;
        #endregion

        /// <summary>
        /// Constructor for HullMap. Creates an array of Hulls from a TileMap.
        /// </summary>
        /// <param name="pTileMap">The TileMap to use as a template for creating the HullMap.</param>
        public HullMap(TileMap pTileMap)
        {
            // INITIALISE _hulls:
            _hulls = new List<Hull>();
            // ITERATE through all Tiles in the TileMap:
            foreach(Tile t in pTileMap.GetTileMap())
            {
                // IF the Tile is a valid Hull shape:
                if(t.IsHull)
                {
                    // CREATE a new Square Hull:
                    Hull newHull = new Hull(new Vector2(1.0f), new Vector2(-1.0f, 1.0f), new Vector2(-1.0f), new Vector2(1.0f, -1.0f)); // Square Hull
                    // SET the position of the Hull to the Til: 
                    newHull.Position = t.EntityLocn;
                    newHull.Scale = new Vector2(GameContent.DEFAULT_TILE_WIDTH,
                                                GameContent.DEFAULT_TILE_HEIGHT);
                    _hulls.Add(newHull);
                }
            }
        }

        public List<Hull> GetHulls()
        {
            return _hulls;
        }
    }
}
