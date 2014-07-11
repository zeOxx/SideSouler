#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace SideSouler_v2.Level
{
    class EditorCamera
    {
        #region Fields
        private const float zoomLimitUpper = 1.5f;
        private const float zoomLimitLower = 0.5f;

        public Vector2 position;
        public float speed;
        public int viewportWidth;
        public int viewportHeight;

        private Matrix transform;
        private float zoom;
        private int worldWidth;
        private int worldHeight;
        #endregion

        #region Constructor
        public EditorCamera(Viewport viewport, int worldWidth, int worldHeight, float initialZoom)
        {
            Zoom = initialZoom;
            ViewportWidth = viewport.Width;
            ViewportHeight = viewport.Height;
            WorldWidth = worldWidth;
            WorldHeight = worldHeight;
            Speed = 1.0f;

            Position = new Vector2(WorldWidth / 2, WorldHeight / 2);
        }
        #endregion

        #region Accessors
        public Vector2 Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }

        public float Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }

        public int ViewportWidth
        {
            get { return this.viewportWidth; }
            set { this.viewportWidth = value; }
        }

        public int ViewportHeight
        {
            get { return this.viewportHeight; }
            set { this.viewportHeight = value; }
        }

        private Matrix Transform
        {
            get { return this.transform; }
            set { this.transform = value; }
        }

        private float Zoom
        {
            get { return this.zoom; }
            set { this.zoom = (float)MathHelper.Clamp(value, zoomLimitLower, zoomLimitUpper); }
        }

        private int WorldWidth
        {
            get { return this.worldWidth; }
            set { this.worldWidth = value; }
        }

        private int WorldHeight
        {
            get { return this.worldHeight; }
            set { this.worldHeight = value; }
        }
        #endregion

        #region Methods
        public void moveCamera(Vector2 amount)
        {
            Vector2 tempPosition = Position;
            tempPosition -= (amount * Speed);

            // Restricting the camera so it doesn't go out of bounds
            //  X-axis:
            if (tempPosition.X < (ViewportWidth / 2))
                tempPosition.X = (ViewportWidth / 2);
            else if (tempPosition.X > (WorldWidth - (ViewportWidth / 2)))
                tempPosition.X = (WorldWidth - (ViewportWidth / 2));

            // Y-axis:
            if (tempPosition.Y < (ViewportHeight / 2))
                tempPosition.Y = (ViewportHeight / 2);
            else if (tempPosition.Y > (WorldHeight - (ViewportHeight / 2)))
                tempPosition.Y = (WorldWidth - (ViewportHeight / 2));

            Position = tempPosition;
        }

        public void zoomCamera(float amount)
        {
            Zoom += amount;
        }

        public void resetZoom()
        {
            Zoom = 1.0f;
        }

        public Matrix getTransformation()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                        Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                        Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));

            return Transform;
        }

        public Matrix getInverseTransform()
        {
            return Matrix.Invert(Transform);
        }
        #endregion
    }
}
