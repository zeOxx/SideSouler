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
    class MenuButton
    {
        #region Fields
        private Texture2D texture;
        private Vector2 position;
        private String text;
        private bool toggle;
        #endregion

        #region Constructor
        public MenuButton(Texture2D texture, Vector2 position, String text, bool toggle)
        {
            this.texture = texture;
            this.position = position;
            this.text = text;
            this.toggle = toggle;
        }
        #endregion
    }
}
