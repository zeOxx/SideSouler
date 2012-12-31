using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SideSouler.Interface
{
    class DialogBox
    {
        #region Fields
        public int width;
        public int height;

        private Vector2 position;
        private int duration;       // In milliseconds
        private int fadeDuration;   // Also in millis

        // Rectangles that make up the box
        private Rectangle boxTop;
        private Rectangle boxBottom;
        private Rectangle boxLeft;
        private Rectangle boxRight;
        private Rectangle boxCorner1;
        private Rectangle boxCorner2;
        private Rectangle boxCorner3;
        private Rectangle boxCorner4;
        private Rectangle boxCenter;

        // Textures the box is made up of
        private Texture2D textureFrameTop;
        private Texture2D textureFrameBottom;
        private Texture2D textureFrameLeft;
        private Texture2D textureFrameRight;
        private Texture2D textureCorner1;
        private Texture2D textureCorner2;
        private Texture2D textureCorner3;
        private Texture2D textureCorner4;
        private Texture2D textureCenter;

        // Fonts
        private SpriteFont headerFont;
        private SpriteFont textFont;

        // Textvariables
        private string headerText;
        private string text;
        private bool hasHeader;
        private bool hasText;
        #endregion

        #region Constructor
        public DialogBox(ContentManager content, Vector2 position, int w, int h, int duration, int fade, int margin)
        {
            Position        = position;
            Width           = w;
            Height          = h;
            Duration        = duration;
            FadeDuration    = fade;

            setupTextures(content);
            updateRectangles(margin);

            HeaderFont  = content.Load<SpriteFont>("Fonts\\headerFont");
            TextFont    = content.Load<SpriteFont>("Fonts\\defaultFont");

            HasHeader = false;
        }
        #endregion

        #region Accessors
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        private Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        private int Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        private int FadeDuration
        {
            get { return this.fadeDuration; }
            set { this.fadeDuration = value; }
        }

        private Rectangle BoxTop
        {
            get { return this.boxTop; }
            set { this.boxTop = value; }
        }

        private Rectangle BoxBottom
        {
            get { return this.boxBottom; }
            set { this.boxBottom = value; }
        }

        private Rectangle BoxLeft
        {
            get { return this.boxLeft; }
            set { this.boxLeft = value; }
        }

        private Rectangle BoxRight
        {
            get { return this.boxRight; }
            set { this.boxRight = value; }
        }

        private Rectangle BoxCorner1
        {
            get { return this.boxCorner1; }
            set { this.boxCorner1 = value; }
        }

        private Rectangle BoxCorner2
        {
            get { return this.boxCorner2; }
            set { this.boxCorner2 = value; }
        }

        private Rectangle BoxCorner3
        {
            get { return this.boxCorner3; }
            set { this.boxCorner3 = value; }
        }

        private Rectangle BoxCorner4
        {
            get { return this.boxCorner4; }
            set { this.boxCorner4 = value; }
        }

        private Rectangle BoxCenter
        {
            get { return this.boxCenter; }
            set { this.boxCenter = value; }
        }

        private Texture2D TextureFrameTop
        {
            get { return this.textureFrameTop; }
            set { this.textureFrameTop = value; }
        }

        private Texture2D TextureFrameBottom
        {
            get { return this.textureFrameBottom; }
            set { this.textureFrameBottom = value; }
        }

        private Texture2D TextureFrameLeft
        {
            get { return this.textureFrameLeft; }
            set { this.textureFrameLeft = value; }
        }

        private Texture2D TextureFrameRight
        {
            get { return this.textureFrameRight; }
            set { this.textureFrameRight = value; }
        }

        private Texture2D TextureCorner1
        {
            get { return this.textureCorner1; }
            set { this.textureCorner1 = value; }
        }
        
        private Texture2D TextureCorner2
        {
            get { return this.textureCorner2; }
            set { this.textureCorner2 = value; }
        }
        
        private Texture2D TextureCorner3
        {
            get { return this.textureCorner3; }
            set { this.textureCorner3 = value; }
        }
        
        private Texture2D TextureCorner4
        {
            get { return this.textureCorner4; }
            set { this.textureCorner4 = value; }
        }

        private Texture2D TextureCenter
        {
            get { return this.textureCenter; }
            set { this.textureCenter = value; }
        }

        private SpriteFont HeaderFont
        {
            get { return this.headerFont; }
            set { this.headerFont = value; }
        }

        private SpriteFont TextFont
        {
            get { return this.textFont; }
            set { this.textFont = value; }
        }

        private string HeaderText
        {
            get { return this.headerText; }
            set { this.headerText = value; }
        }

        private string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        private bool HasHeader
        {
            get { return this.hasHeader; }
            set { this.hasHeader = value; }
        }

        private bool HasText
        {
            get { return this.hasText; }
            set { this.hasText = value; }
        }
        #endregion

        #region Methods
        public void setupTextures(ContentManager content)
        {
            TextureFrameTop = content.Load<Texture2D>("Interface\\boxFrameTop");
            TextureFrameBottom = content.Load<Texture2D>("Interface\\boxFrameBottom");
            TextureFrameLeft = content.Load<Texture2D>("Interface\\boxFrameLeft");
            TextureFrameRight = content.Load<Texture2D>("Interface\\boxFrameRight");
            TextureCorner1 = content.Load<Texture2D>("Interface\\boxFrameCorner");
            TextureCorner2 = content.Load<Texture2D>("Interface\\boxFrameCorner2");
            TextureCorner3 = content.Load<Texture2D>("Interface\\boxFrameCorner3");
            TextureCorner4 = content.Load<Texture2D>("Interface\\boxFrameCorner4");
            TextureCenter = content.Load<Texture2D>("Interface\\boxFrameCenter");
        }

        public void update(Vector2 cameraPosition, int margin)
        {
            updatePosition(cameraPosition);
            updateRectangles(margin);
        }

        public void updatePosition(Vector2 cameraPosition)
        {
            Position = cameraPosition;
        }
        
        public void updateRectangles(int margin)
        {
            BoxTop      = new Rectangle((int)Position.X + (TextureCorner1.Width), (int)Position.Y, Width - (TextureCorner1.Width + margin), TextureFrameTop.Height);
            BoxCenter   = new Rectangle((int)Position.X + (TextureCorner1.Width), (int)Position.Y + (TextureFrameTop.Height), Width - (TextureFrameLeft.Width + margin), Height - (TextureFrameTop.Height * 2));
            BoxBottom   = new Rectangle((int)Position.X + (TextureCorner1.Width), (int)Position.Y + (BoxCenter.Height + TextureFrameTop.Height), Width - (TextureCorner1.Width + margin), TextureFrameTop.Height);
            BoxLeft     = new Rectangle((int)Position.X, (int)Position.Y + (TextureCorner1.Height), TextureFrameLeft.Width, BoxCenter.Height);
            BoxRight    = new Rectangle((int)Position.X + (TextureFrameLeft.Width + BoxCenter.Width), (int)Position.Y + (TextureCorner1.Height), TextureFrameLeft.Width, BoxCenter.Height);
            BoxCorner1  = new Rectangle((BoxTop.X + BoxTop.Width), (int)Position.Y, TextureCorner1.Width, TextureCorner1.Height);
            BoxCorner2  = new Rectangle((BoxBottom.X + BoxBottom.Width), BoxBottom.Y, TextureCorner2.Width, TextureCorner2.Height);
            BoxCorner3  = new Rectangle((BoxBottom.X - TextureCorner3.Width), BoxBottom.Y, TextureCorner3.Width, TextureCorner3.Height);
            BoxCorner4  = new Rectangle((int)Position.X, (int)Position.Y, TextureCorner4.Width, TextureCorner4.Height);
        }
        
        public void setHeaderText(string text)
        {
            HeaderText = text;

            HasHeader = true;
        }

        public void setText(string text)
        {
            Text = text;

            HasText = true;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            // Draws the box
            spriteBatch.Draw(TextureCenter, BoxCenter, Color.White);
            spriteBatch.Draw(TextureFrameTop, BoxTop, Color.White);
            spriteBatch.Draw(TextureFrameBottom, BoxBottom, Color.White);
            spriteBatch.Draw(TextureFrameLeft, BoxLeft, Color.White);
            spriteBatch.Draw(TextureFrameRight, BoxRight, Color.White);
            spriteBatch.Draw(TextureCorner1, BoxCorner1, Color.White);
            spriteBatch.Draw(TextureCorner2, BoxCorner2, Color.White);
            spriteBatch.Draw(TextureCorner3, BoxCorner3, Color.White);
            spriteBatch.Draw(TextureCorner4, BoxCorner4, Color.White);

            Vector2 headerPosition;
            headerPosition.X = Position.X + 5;
            headerPosition.Y = Position.Y + 3;

            // Draws the text, if it is present
            if (HeaderText != null)
            {
                spriteBatch.DrawString(HeaderFont, HeaderText, headerPosition, Color.White);
            }

            if (Text != null)
            {
                spriteBatch.DrawString(TextFont, Text, Position, Color.White);
            }
        }
        #endregion
    }
}
