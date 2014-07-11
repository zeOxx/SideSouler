#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace SideSouler_v2.Systems
{
    class InputHandler
    {
        #region Fields
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        MouseState currentMouseState;
        MouseState previousMouseState;
        #endregion

        #region Constructor
        public InputHandler()
        {
            CurrentKeyboardState = Keyboard.GetState();

            CurrentMouseState = Mouse.GetState();
        }
        #endregion

        #region Accessors
        public KeyboardState CurrentKeyboardState
        {
            get { return this.currentKeyboardState; }
            set { this.currentKeyboardState = value; }
        }

        public KeyboardState PreviousKeyboardState
        {
            get { return this.previousKeyboardState; }
            set { this.previousKeyboardState = value; }
        }

        public MouseState CurrentMouseState
        {
            get { return this.currentMouseState; }
            set { this.currentMouseState = value; }
        }
        public MouseState PreviousMouseState
        {
            get { return this.previousMouseState; }
            set { this.previousMouseState = value; }
        }
        #endregion

        #region Methods
        public void update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        #region Keyboard
        public bool keyReleased(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);
        }

        public bool keyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }
        #endregion

        #region Mouse
        public bool leftClicked()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool rightClicked()
        {
            return CurrentMouseState.RightButton == ButtonState.Pressed; ;
        }

        public Vector2 mousePosition()
        {
            return new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
        }

        public Vector2 mouseMoved()
        {
            Vector2 moved = Vector2.Zero;

            if (CurrentMouseState.X != PreviousMouseState.X)
                moved.X = (CurrentMouseState.X - PreviousMouseState.X);
            if (CurrentMouseState.Y != PreviousMouseState.Y)
                moved.Y = (CurrentMouseState.Y - PreviousMouseState.Y);

            return moved;
        }

        public float scrollwheelMoved()
        {
            if (CurrentMouseState.ScrollWheelValue > PreviousMouseState.ScrollWheelValue)
                return 0.05f;
            else if (CurrentMouseState.ScrollWheelValue < PreviousMouseState.ScrollWheelValue)
                return -0.05f;
            else
                return 0;
        }
        #endregion

        #endregion
    }
}
