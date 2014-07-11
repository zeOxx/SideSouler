#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SideSouler.Interface
{
    class Element
    {
        #region Fields
        private Texture2D texture;

        /* This may be a little weird, but the position is actually relative to the position of the camera.
         * for example:
         * 
         * If the position of the element in the camera itself should be (300, 300), the position you pass as an argument
         * would be (-340, -60). This is because the position passed in is applied to the center of the camera, which in this
         * case is (640, 360).
         */
        private Vector2 position;
        private Vector2 drawPosition;
        #endregion

        #region Constructor
        // Use this to get a standard UI Element
        public Element(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }
        #endregion

        #region Accessors
        private Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        private Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        private Vector2 DrawPosition
        {
            get { return this.drawPosition; }
            set { this.drawPosition = value; }
        }
        #endregion

        #region Methods
        public void update(Vector2 cameraPosition)
        {
            DrawPosition = cameraPosition + Position;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DrawPosition, Color.White);
        }
        #endregion
    }
}
