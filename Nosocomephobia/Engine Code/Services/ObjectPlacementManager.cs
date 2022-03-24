using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities;
using Nosocomephobia.Game_Code.World;
using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 20-03-22
/// </summary>
namespace Nosocomephobia.Engine_Code.Services
{
    /// <summary>
    /// Class ObjectPlacementManager. Responsible for randomly placing objects in their respective quadrants.
    /// </summary>
    public class ObjectPlacementManager : IObjectPlacementManager
    {
        #region FIELDS
        // DECLARE 4 List<Tile>, one for each quadrant:
        private List<Tile> _quadrantOneTiles;
        private List<Tile> _quadrantTwoTiles;
        private List<Tile> _quadrantThreeTiles;
        private List<Tile> _quadrantFourTiles;
        // DECLARE a List<Artefact>, call it _artefacts:
        private List<Artefact> _artefacts;
        // DECLARE a TileMap, used to store a reference to the FloorTileMap;
        private TileMap _floorTileMap;
        #endregion

        #region PROPERTIES
        // DECLARE a get-set property for the List<Artefact> Artefacts:
        public List<Artefact> Artefacts
        {
            get { return _artefacts; }
            set { _artefacts = value; }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor for ObjectPlacementManager
        /// </summary>
        public ObjectPlacementManager(TileMap pFloorTileMap)
        {
            // INITIALISE fields:
            _quadrantOneTiles = new List<Tile>();
            _quadrantTwoTiles = new List<Tile>();
            _quadrantThreeTiles = new List<Tile>();
            _quadrantFourTiles = new List<Tile>();
            _artefacts = new List<Artefact>();
            _floorTileMap = pFloorTileMap;

            // POPULATE the Quadrants with Tiles:
            this.PopulateQuadrants();
        }

        /// <summary>
        /// Populates each Quadrant List with their respective Tiles.
        /// </summary>
        private void PopulateQuadrants()
        {
            // ITERATE through the _floorTileMap:
            foreach (Tile tile in _floorTileMap.GetTileMap())
            {
                // IF the Tile belongs to quadrant 1:
                if (tile.TileQuadrant == 1)
                {
                    // ADD it to _quadrantOneTiles:
                    _quadrantOneTiles.Add(tile);
                }
                // IF the Tile belongs to quadrant 2:
                if (tile.TileQuadrant == 2)
                {
                    // ADD it to _quadrantTwoTiles:
                    _quadrantTwoTiles.Add(tile);
                }
                // IF the Tile belongs to quadrant 3:
                if (tile.TileQuadrant == 3)
                {
                    // ADD it to _quadrantThreeTiles:
                    _quadrantThreeTiles.Add(tile);
                }
                // IF the Tile belongs to quadrant 4:
                if (tile.TileQuadrant == 4)
                {
                    // ADD it to _quadrantFourTiles:
                    _quadrantFourTiles.Add(tile);
                }
            }
        }

        /// <summary>
        /// Randomises the placements of Artefacts in their respective quadrants.
        /// </summary>
        public void RandomiseArtefactPlacements()
        {
            // DECLARE an instance of Random, call it random:
            Random random = new Random();

            // GET an index for a Tile in Quadrant 1:
            int q1RandomTileIndex = random.Next(0, _quadrantOneTiles.Count);
            // SET the 1st Artefact to the location of the random Tile:
            _artefacts[0].EntityLocn = _quadrantOneTiles[q1RandomTileIndex].EntityLocn;

            // GET an index for a Tile in Quadrant 2:
            int q2RandomTileIndex = random.Next(0, _quadrantTwoTiles.Count);
            // SET the 2nd Artefact to the location of the random Tile:
            _artefacts[1].EntityLocn = _quadrantTwoTiles[q2RandomTileIndex].EntityLocn;

            // GET an index for a Tile in Quadrant 3:
            int q3RandomTileIndex = random.Next(0, _quadrantThreeTiles.Count);
            // SET the 3rd Artefact to the location of the random Tile:
            _artefacts[2].EntityLocn = _quadrantThreeTiles[q3RandomTileIndex].EntityLocn;

            // GET an index for a Tile in Quadrant 4:
            int q4RandomTileIndex = random.Next(0, _quadrantFourTiles.Count);
            // SET the 4th Artefact to the location of the random Tile:
            _artefacts[3].EntityLocn = _quadrantFourTiles[q4RandomTileIndex].EntityLocn;
        }
        #endregion
    }
}
