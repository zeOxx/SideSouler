#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SideSouler.Level
{
    class TileSheet
    {
        #region Fields
        private Texture2D texture;

        private int sheetWidth;
        private int sheetHeight;
        private int tileSize;

        private Rectangle sourceRectangle;
        #endregion

        #region Constructor
        public TileSheet(Texture2D sheet, int sheetWidth, int sheetHeight, int tileSize)
        {
            Texture = sheet;
            SheetWidth = sheetWidth;
            sheetWidth = sheetHeight;
            TileSize = tileSize;
        }
        #endregion

        #region Accessors
        private Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        private int SheetWidth
        {
            get { return this.sheetWidth; }
            set { this.sheetWidth = value; }
        }

        private int SheetHeight
        {
            get { return this.sheetHeight; }
            set { this.sheetHeight = value; }
        }

        private int TileSize
        {
            get { return this.tileSize; }
            set { this.tileSize = value; }
        }

        private Rectangle SourceRectangle
        {
            get { return this.sourceRectangle; }
            set { this.sourceRectangle = value; }
        }
        #endregion

        #region Methods
        // Method that returns a Tile in a specific location on the tilesheet
        public Tile getTile(int x, int y)
        {
            Tile tile;

            if (!((x * TileSize) > (SheetWidth / TileSize) || (y * TileSize) > (SheetHeight / TileSize)))
            {
                SourceRectangle = new Rectangle((x * TileSize), (y * TileSize), TileSize, TileSize);

                tile = new Tile(Texture, SourceRectangle, true, false);

                return tile;
            }
            else
                return null;
        }
        #endregion
    }
}
