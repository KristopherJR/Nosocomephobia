using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.3, 20-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.World
{
    public class TileMap
    {
        // DECLARE a 2-Dimensional Array, call it tileMap:
        private Tile[,] tileMap;
        // DECLARE a String, call it tileMapFilePath. This will contain the system path to the .csv file containing the TileMap data:
        private String tileMapFilePath;
        // DECLARE a bool, call it isLayerCollidable:
        private bool isLayerCollidable;
        private StreamWriter _streamWriter;
        private string _outputPath;

        /// <summary>
        /// Constructor for objects of class TileMap.
        /// </summary>
        /// <param name="tileMapFilePath">The System File Path to the .csv file containing the TileMap data.</param>
        /// <param name="isLayerCollidable">A boolean specifying if objects can collide with this Layer.</param>
        public TileMap(String tileMapFilePath, bool isLayerCollidable)
        {
            _outputPath = Directory.GetCurrentDirectory() + "/TilePositions.csv";
            // SET fields to the incoming parameters:
            this.tileMapFilePath = tileMapFilePath;
            this.isLayerCollidable = isLayerCollidable;
            // LOAD the TileMap:
            this.LoadTileMap();
        }

        /// <summary>
        /// Loads the TileMap from the data in the .csv File.
        /// </summary>
        private void LoadTileMap()
        {
            // DECLARE a List<String>, call it rows. Set it to the return of ParseFile(tileMapFilePath):
            List<String> rows = this.ParseFile(tileMapFilePath);
            // DECLARE an int, call it width. Set it to the Count of values in the text file on the X axis:
            int width = rows[0].Split(',').Count();
            // DECLARE an int, call it height. Set it to the Count of values in the text file on the Y axis:
            int height = rows.Count;
            // INITALISE the tileMap and pass in the width and height:
            tileMap = new Tile[width, height];
            // LOOP for height:
            for (int y = 0; y < height; y++)
            {
                // DECLARE a String[], call it tileStrings. Set it to the rows[y] split by commas:
                String[] tileStrings = rows[y].Split(',');
                // LOOP for tileStrings.Length (I.E the entire text row):
                for (int x = 0; x < tileStrings.Length; x++)
                {
                    // DECLARE an int, call it tileIDParse and set it to the value at tileStrings[x]. Convert it from a String to an int:
                    int tileIdParse = int.Parse(tileStrings[x]);
                    // DECLARE a bool, call it isValidTile and set it to true:
                    bool isValidTile = true;
                    // DECLARE a bool, call it isHull and set it to true:
                    bool isHull = false;
                    // DECLARe an int, call it quadrant:
                    int quadrant = 0;
                    // IF tileIdParse < 0:
                    if (tileIdParse < 0)
                    {
                        // This section is used to stop invisible tiles being loaded into the TileMap:
    
                        // SET tileIdParse to the total number of Tiles - 1:
                        tileIdParse = (GameContent.NUMBER_OF_TILES) - 1;
                        // SET isValidTile to false:
                        isValidTile = false;
                        
                    }
                    else
                    {
                        isHull = true;
                    }
                    // CHECK the tileID, if its 35 then the tile belongs to quadrant 1:
                    if (tileIdParse == 35)
                    {
                        quadrant = 1;
                    }
                    // CHECK the tileID, if its 0 then the tile belongs to quadrant 2:
                    if (tileIdParse == 0)
                    {
                        quadrant = 2;
                    }
                    // CHECK the tileID, if its 85 then the tile belongs to quadrant 3:
                    if (tileIdParse == 85)
                    {
                        quadrant = 3;
                    }
                    // CHECK the tileID, if its 110 then the tile belongs to quadrant 4:
                    if (tileIdParse == 110)
                    {
                        quadrant = 4;
                    }
                    
                    // DECLARE a Tile, call it newTile and pass in tileIdParse:
                    Tile newTile = new Tile(tileIdParse);
                    // SET the tiles Quadrant:
                    newTile.TileQuadrant = quadrant;
                    // SET newTile.IsCollidable to true if the layer is collidable and it's a valid tile, else false:
                    newTile.IsCollidable = isLayerCollidable && isValidTile;
                    // CHECK if the tile is valid based on its parse, if its not set the property to false:
                    if(isValidTile == false)
                    {
                        newTile.IsValidTile = false;
                    }
                    // SET the tiles position to the next space in the TileMap:
                    newTile.SetTilePosition(new Vector2(x * GameContent.DEFAULT_TILE_WIDTH,
                                                        y * GameContent.DEFAULT_TILE_HEIGHT));
                    newTile.IsHull = isHull;
                    // STORE the newly created Tile in the TileMap:
                    tileMap[x, y] = newTile;
                }
            }
        }

        public void WriteTilePositionsToText()
        {
            using (_streamWriter = new StreamWriter(_outputPath))
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    for (int x = 0; x < tileMap.GetLength(0); x++)
                    {
                        _streamWriter.Write(tileMap[x, y].EntityLocn + ",");
                    }
                    _streamWriter.WriteLine(""); // new line
                }
            }
        }
        /// <summary>
        /// Reads a File and returns it as a List<String>.
        /// </summary>
        /// <param name="filePath">The System File Path to the File.</param>
        /// <returns>A List<String> containing all of the data in the File.</returns>
        private List<String> ParseFile(String filePath)
        {
            // DECLARE a List<String>, call it lines and initalise it:
            List<String> lines = new List<String>();
            // USE a StreamReader to read in the File at the filePath:
            using (StreamReader reader = new StreamReader(filePath))
            {
                // DECLARE a String, call it line:
                String line;
                // WHILST there is a new line in the File:
                while ((line = reader.ReadLine()) != null)
                {
                    // ADD the line to the lines List:
                    lines.Add(line);
                }
            }
            // RETURN lines:
            return lines;
        }

        /// <summary>
        /// Returns a Tile from the TileMap at the given index.
        /// </summary>
        /// <param name="xIndex">The X index of the Tile to retrieve.</param>
        /// <param name="yIndex">The Y index of the Tile to retrieve.</param>
        /// <returns>The Tile at the specified index from the TileMap.</returns>
        public Tile GetTileAtIndex(int xIndex, int yIndex)
        {
            // RETURN the Tile from the TileMap at the specified index:
            return tileMap[xIndex, yIndex];
        }
        /// <summary>
        /// Draws the TileMap onto the SpriteBatch provided as a parameter.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw the TileMap onto.</param>
        public void DrawTileMap(SpriteBatch spriteBatch)
        {
            // FOREACH Tile in TileMap:
            foreach (Tile t in tileMap)
            {
                // IF the tile is valid:
                if(t.IsValidTile)
                {
                    // DRAW the Tile:
                    t.Draw(spriteBatch);
                }
            }
        }
        /// <summary>
        /// Returns the Whole TileMap.
        /// </summary>
        /// <returns>Returns the Whole TileMap.</returns>
        public Tile[,] GetTileMap()
        {
            // RETURN tileMap:
            return tileMap;
        }
    }
}
