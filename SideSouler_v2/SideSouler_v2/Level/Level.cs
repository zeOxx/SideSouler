/************************************************************
 * SideSouler v2                                            *
 ************************************************************
 *  Level.cs                                                *
 * This class handles everything that a level can hold.     *
 * Right now the code is very badly commented,              *
 * sorry :(                                                 *
 ************************************************************
 * By Inge Dalby, 2014                                      *
 ************************************************************/

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SideSouler_v2.Level
{
    class Level
    {
        #region Fields
        private Vector2 size;               // Size can ONLY be given as integers to the constructor.
        private List<Vector2> layerTwoPositions;
        private List<Tile> tilesLayerOne;      // Background layer
        private List<Tile> tilesLayerTwo;      // Player layer
        private List<Tile> tilesLayerThree;    // Foreground layer
        private List<Tile> tilesLayerGrid;
        private int currentTileOne;
        private int currentTileTwo;
        private int currentTileThree;
        private String levelName;          // The level name has the following structure: "(world)-(level)_(name)". Example: "1-3_Undead Parish"

        //the rest is commented out for the time being, as it has yet to be implemented
        #region Commented out fields
        // private Enemy[] enemies;
        // private NPC[] npcs;
        // private GameObject[] gameObjects;
        #endregion
        #endregion

        #region Constructor
        public Level(Vector2 size, String levelName)
        {
            Size = size;
            LevelName = levelName;

            CurrentTileOne = 0;
            CurrentTileTwo = 0;
            CurrentTileThree = 0;

            setTileSize();
        }
        #endregion

        #region Accessors
        private Vector2 Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        private List<Tile> TilesLayerOne
        {
            get { return this.tilesLayerOne; }
            set { this.tilesLayerOne = value; }
        }

        private List<Tile> TilesLayerTwo
        {
            get { return this.tilesLayerTwo; }
            set { this.tilesLayerTwo = value; }
        }

        private List<Tile> TilesLayerThree
        {
            get { return this.tilesLayerThree; }
            set { this.tilesLayerThree = value; }
        }

        private List<Tile> TilesLayerGrid
        {
            get { return this.tilesLayerGrid; }
            set { this.tilesLayerGrid = value; }
        }

        private int CurrentTileOne
        {
            get { return this.currentTileOne; }
            set { this.currentTileOne = value; }
        }

        private int CurrentTileTwo
        {
            get { return this.currentTileTwo; }
            set { this.currentTileTwo = value; }
        }

        private int CurrentTileThree
        {
            get { return this.currentTileThree; }
            set { this.currentTileThree = value; }
        }

        private String LevelName
        {
            get { return this.levelName; }
            set { this.levelName = value; }
        }
        #endregion

        #region Methods
        private void setTileSize()
        {
            TilesLayerOne = new List<Tile>();
            TilesLayerTwo = new List<Tile>();
            TilesLayerThree = new List<Tile>();
            TilesLayerGrid = new List<Tile>();
            layerTwoPositions = new List<Vector2>();
        }

        #region Tile placement and removal
        public void placeTile(Tile tile, Vector2 position, int layer)
        {
            Tile tempTile = tile;

            switch (layer)
            {
                case 1:
                    if (CurrentTileOne < (Size.X * Size.Y))
                    {
                        if (!positionCheck(position, TilesLayerOne))
                        {
                            TilesLayerOne.Add(tempTile);

                            CurrentTileOne++;
                        }
                    }
                    break;
                case 2:
                    if (CurrentTileTwo < (Size.X * Size.Y))
                    {
                        if (!positionCheck(position, TilesLayerTwo))
                        {
                            TilesLayerTwo.Add(tempTile);

                            CurrentTileTwo++;
                        }
                    }
                    break;
                case 3:
                    if (CurrentTileThree < (Size.X * Size.Y))
                    {
                        if (!positionCheck(position, TilesLayerThree))
                        {
                            TilesLayerThree.Add(tempTile);

                            CurrentTileThree++;
                        }
                    }
                    break;
            }
        }

        public void removeTileCheck(Vector2 position, int layer)
        {
            switch (layer)
            {
                case 1:
                    removeTile(position, TilesLayerOne);
                    CurrentTileOne--;
                    break;
                case 2:
                    removeTile(position, TilesLayerTwo);
                    CurrentTileTwo--;
                    break;
                case 3:
                    removeTile(position, TilesLayerThree);
                    CurrentTileThree--;
                    break;
            }
        }
        #endregion

        public void populateWithGrid(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Env\\Dev\\devTileSheet.png");

            Vector2 tilePosition = new Vector2(0, 0);
            Rectangle source;

            for (int i = 0; i < Size.Y; i++)
            {
                tilePosition.X = 0;

                for (int j = 0; j < Size.X; j++)
                {
                    source = new Rectangle(0, 0, 32, 32);

                    Tile tempTile = new Tile(texture, source, true, true);
                    tempTile.setPosition(tilePosition);

                    tilePosition.X += 32;

                    TilesLayerGrid.Add(tempTile);
                }

                tilePosition.Y += 32;
            }
        }

        // Used to check if the position of a tile is already taken.
        private bool positionCheck(Vector2 position, List<Tile> listToCheck)
        {
            bool occupied = false;

            for (int i = 0; i < listToCheck.Count; i++)
            {
                if (position == listToCheck[i].Position)
                    occupied = true;
            }

            return occupied;
        }

        private void removeTile(Vector2 position, List<Tile> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (position == list[i].Position)
                    list.RemoveAt(i);
            }
        }

        #region Draw methods
        public void drawLayerOne(SpriteBatch spriteBatch, float alpha)
        {
            for (int i = 0; i < TilesLayerOne.Count; i++)
            {
                if (TilesLayerOne[i] != null || TilesLayerOne[i].PoisitionSet != false)
                    TilesLayerOne[i].draw(spriteBatch, alpha);
            }
        }

        public void drawLayerTwo(SpriteBatch spriteBatch, float alpha)
        {
            for (int i = 0; i < TilesLayerTwo.Count; i++)
            {
                if (TilesLayerTwo[i] != null || TilesLayerOne[i].PoisitionSet != false)
                    TilesLayerTwo[i].draw(spriteBatch, alpha);
            }
        }

        public void drawLayerThree(SpriteBatch spriteBatch, float alpha)
        {
            for (int i = 0; i < TilesLayerThree.Count; i++)
            {
                if (TilesLayerThree[i] != null || TilesLayerOne[i].PoisitionSet != false)
                    TilesLayerThree[i].draw(spriteBatch, alpha);
            }
        }

        public void drawGrid(SpriteBatch spriteBatch, float alpha)
        {
            for (int i = 0; i < TilesLayerGrid.Count; i++)
            {
                if (TilesLayerGrid[i] != null || TilesLayerOne[i].PoisitionSet != false)
                    TilesLayerGrid[i].draw(spriteBatch, alpha);
            }
        }
        #endregion

        #endregion
    }
}
