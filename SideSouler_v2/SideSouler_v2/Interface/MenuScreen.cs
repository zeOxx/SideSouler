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
    class MenuScreen
    {
        #region Fields
        private enum MenuType { MAIN, OPTIONS, PAUSE, CREDITS };
        MenuType menuType;

        List<MenuButton> menuButtonList;
        #endregion

        #region Constructor
        public MenuScreen(ContentManager content, String type)
        {
            setMenuType(type);

            menuButtonList = new List<MenuButton>();
        }
        #endregion

        #region Methods
        // Method that parses the string 'type' into a valid MenuType.
        // If 'type' is invalid, an error is printed in console and the
        //  application quits when escape is pressed (not yet implemented)
        private void setMenuType(string type)
        {
            MenuType tempType;
            if (Enum.TryParse(type, out tempType))
                menuType = tempType;
            /*else
                IMPLEMENT ERROR CLASS AND PRINT TO CONSOLE
             */
        }

        // This method will add MenuButton objects into the list
        public void addMenuButton(MenuButton button)
        {
            menuButtonList.Add(button);
        }
        #endregion
    }
}
