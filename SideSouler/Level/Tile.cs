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
        public bool poisitionSet;

        private Rectangle   sourceRectangle;
        private Texture2D   texture;
        private Vector2     origin;
        private bool        solid;
        private bool        interactable;
        #endregion

        #region Constructor
        public Tile(Texture2D texture, Rectangle sourceRectangle, bool solid, bool interactable)
        {
            Texture         = texture;
            SourceRectangle = sourceRectangle;
            Solid           = solid;
            Interactable    = interactable;
            Origin          = Vector2.Zero;

            PoisitionSet = false;
        }
        #endregion

        #region Accessors
        public Vector2 Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }

        public bool PoisitionSet
        {
            get { return this.poisitionSet; }
            set { this.poisitionSet = value; }
        }

        private Rectangle SourceRectangle
        {
            get { return this.sourceRectangle; }
            set { this.sourceRectangle = value; }
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
        #endregion

        #region Methods
        public void setPosition(Vector2 position)
        {
            Position = position;

            PoisitionSet = true;
        }

        public void draw(SpriteBatch spriteBatch, float alpha)
        {
            spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White * alpha);
        }
        #endregion
    }
}
