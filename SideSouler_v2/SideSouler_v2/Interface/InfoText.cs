#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SideSouler_v2.Interface
{
    class InfoText
    {
        #region Fields
        private String text;
        private Vector2 position;
        private Vector2 offset;
        private SpriteFont font;
        #endregion

        #region Constructor
        // This class displays text anywhere on the screen. Position should be given relative to the cameras position
        public InfoText(ContentManager content, String text, Vector2 offset)
        {
            Font = content.Load<SpriteFont>("Fonts\\defaultFont");
            Text = text;
            Offset = offset;
        }
        #endregion

        #region Accessors
        private String Text
        {
            get { return this.text; }
            set { this.text = value; }
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

        private SpriteFont Font
        {
            get { return this.font; }
            set { this.font = value; }
        }
        #endregion

        #region Methods
        public void update(Vector2 cameraPosition)
        {
            Position = cameraPosition + Offset;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color.White);
        }
        #endregion
    }
}
