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
        public int sheetWidth;
        public int sheetHeight;

        // Position and Offset will ONLY be used when in the editor. It is used to display the tilesheet
        //  so the user can see and select tiles from it.
        private Vector2 position;
        private Vector2 offset;

        private Texture2D texture;
        private int tileSize;

        private Rectangle sourceRectangle;
        #endregion

        #region Constructor
        public TileSheet(Texture2D sheet, int sheetWidth, int sheetHeight, int tileSize, Vector2 offset)
        {
            Texture = sheet;
            SheetWidth = sheetWidth;
            sheetWidth = sheetHeight;
            TileSize = tileSize;

            Offset = offset;
        }
        #endregion

        #region Accessors
        public int SheetWidth
        {
            get { return this.sheetWidth; }
            set { this.sheetWidth = value; }
        }

        public int SheetHeight
        {
            get { return this.sheetHeight; }
            set { this.sheetHeight = value; }
        }

        private Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        private Vector2 Offset
        {
            get { return this.offset; }
            set { this.offset = value; }
        }

        private Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
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
        public void update(Vector2 cameraPosition)
        {
            Position = cameraPosition + Offset;
        }

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

        // Draw will ONLY be used when in the editor
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, Color.White);
        }
        #endregion
    }
}
