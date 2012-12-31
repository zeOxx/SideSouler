#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SideSouler.Level
{
    class Tile
    {
        #region Fields
        public Vector2 position;

        private Texture2D   texture;
        private Vector2     origin;
        private bool        solid;
        private bool        interactable;
        private int         type;               // Contains value from enumerator in (INSERT CLASS HERE)
        #endregion

        #region Constructor
        public Tile(Texture2D texture, Vector2 position, bool solid, bool interactable, int type)
        {
            Texture         = texture;
            Position        = position;
            Solid           = solid;
            Interactable    = interactable;
            Type            = type;
            Origin          = Vector2.Zero;
        }
        #endregion

        #region Accessors
        public Vector2 Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }
        
        private Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        private Vector2 Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }

        private bool Solid
        {
            get { return this.solid; }
            set { this.solid = value; }
        }

        private bool Interactable
        {
            get { return this.interactable; }
            set { this.interactable = value; }
        }

        private int Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        #endregion

        #region Methods
        public void draw(SpriteBatch spriteBatch, float alpha)
        {
            spriteBatch.Draw(Texture, Position, Color.White * alpha);
        }
        #endregion
    }
}
