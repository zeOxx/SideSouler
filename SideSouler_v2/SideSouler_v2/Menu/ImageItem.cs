using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SideSouler_v2.Menu
{
    class ImageItem
    {
        private Texture2D texture;
        private Texture2D selectionRect;
        private Vector2 position;
        private Vector2 selRectPos;
        private int selectionMargin;
        private float selectionRectAlpha;
        private bool pulse;             // this one is weird. true is fade out, false is fade in. this effects the alpha of the selection rectangle

        public bool isSelected;

        public ImageItem(Texture2D texture, Vector2 position, ContentManager content)
        {
            selectionMargin = 4;

            this.texture = texture;
            this.position = position;

            setupSelectionRect(content);
        }

        private void setupSelectionRect(ContentManager content)
        {
            selectionRect = content.Load<Texture2D>("Menu\\border");
            selRectPos = new Vector2(position.X - selectionMargin, position.Y - selectionMargin);
            selectionRectAlpha = 1.0f;
            pulse = true;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            if (isSelected)
            {
                spriteBatch.Draw(selectionRect, selRectPos, Color.White * selectionRectAlpha);
                
                // Handles alpha
                if (pulse)
                    selectionRectAlpha -= 0.07f;
                else
                    selectionRectAlpha += 0.07f;

                if (selectionRectAlpha < 0.0f)
                {
                    pulse = false;
                    selectionRectAlpha = 0.0f;
                }
                if (selectionRectAlpha > 1.0f)
                {
                    pulse = true;
                    selectionRectAlpha = 1.0f;
                }                    
            }
        }
    }
}
