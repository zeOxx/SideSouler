/************************************************************
 * SideSouler v2                                            *
 ************************************************************
 *  Item.cs                                                 *
 * Top level class for every item in the game.              *
 * Should never be instanciated by it self.                 *
 ************************************************************
 * By Inge Dalby, 2014                                      *
 ************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SideSouler_v2.Player
{
    class Item
    {
        private bool consumable;
        private bool charges;
        private String name;
        private String description;
        private int amount;          // Amount is used both as a stack and number of charges.
        private int maxAmount;
        private Texture2D icon;

        /*
         * Basic constructor that sets everything to 0.
         */
        public Item()
        {
            consumable = false;
            charges = false;
            name = null;
            description = null;
            amount = 0;
            maxAmount = 0;
            icon = null;
        }

        #region Accessors
        public bool IsConsumable()
        {
            return consumable;
        }

        public bool HasCharges()
        {
            return charges;
        }

        public String getName()
        {
            return name;
        }

        public String getDescription()
        {
            return description;
        }

        public int getAmount()
        {
            return amount;
        }

        public int getMaxAmount()
        {
            return maxAmount;
        }
        #endregion
    }
}
