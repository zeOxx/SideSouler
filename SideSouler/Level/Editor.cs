#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SideSouler.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SideSouler.Systems;
#endregion

namespace SideSouler.Level
{
    class Editor
    {
        #region Fields
        private int width;
        private int height;
        private int margin;
        private int layer;

        private DialogBox topBox;
        private DialogBox rightBox;
        private DialogBox modeBox;

        private Texture2D cursor;
        private Vector2 cursorPosition;

        private Level currentLevel;

        public EditorCamera editorCamera;

        // TODO:
        //  Add object mode and particle mode, among other things (NPC Mode etc)
        private enum Modes
        {
            Edit,
            View
        }

        private Modes mode;
        #endregion

        #region Constructor
        public Editor(Viewport view, ContentManager content, int screenWidth, int screenHeight)
        {
            mode = Modes.View;

            currentLevel = new Level(new Vector2(100, 100), "");
            currentLevel.populateWithGrid(content);

            Width   = 64 * 100;
            Height  = 64 * 100;

            EditorCamera = new EditorCamera(view, Width, Height, 1.0f);

            Margin  = 2;     // MAGIC NUMBERS YAY
            Layer   = 2;

            ModeBox = new DialogBox(content, new Vector2(screenWidth / 2 - 100, screenHeight - 20), 128, 24, 0, 0, Margin);
            ModeBox.setHeaderText("View Mode");

            TopBox = new DialogBox(content, new Vector2(Margin, Margin), screenWidth - (Margin * 2), 25, 0, 0, Margin);
            TopBox.setHeaderText("Editor   -   Layer: " + Layer);

            RightBox = new DialogBox(content, new Vector2(screenWidth - 200, 30), 256, 687, 0, 0, Margin);

            Cursor = content.Load<Texture2D>("Editor\\cursorNormal");
            CursorPosition = Vector2.Zero;
        }
        #endregion

        #region Accessors
        private int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        private int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        private int Margin
        {
            get { return this.margin; }
            set { this.margin = value; }
        }

        private int Layer
        {
            get { return this.layer; }
            set { this.layer = value; }
        }

        private DialogBox TopBox
        {
            get { return this.topBox; }
            set { this.topBox = value; }
        }

        private DialogBox RightBox
        {
            get { return this.rightBox; }
            set { this.rightBox = value; }
        }

        private DialogBox ModeBox
        {
            get { return this.modeBox; }
            set { this.modeBox = value; }
        }

        public Texture2D Cursor
        {
            get { return this.cursor; }
            set { this.cursor = value; }
        }

        public Vector2 CursorPosition
        {
            get { return this.cursorPosition; }
            set { this.cursorPosition = value; }
        }

        public EditorCamera EditorCamera
        {
            get { return this.editorCamera; }
            set { this.editorCamera = value; }
        }
        #endregion

        #region Methods
        public void update(ContentManager content, InputHandler inputHandler)
        {
            /* Changing modes */
            if (inputHandler.keyReleased(Keys.Tab))
                changeMode();

            /* Changing Layers */
            layerChecker(inputHandler);

            /* Change top text*/
            TopBox.setHeaderText("Editor   -   Layer: " + Layer);

            /* Moving camera and updating mouse texture/position + HUD position*/
            if (inputHandler.keyDown(Keys.Space))
            {
                editorCamera.moveCamera(inputHandler.mouseMoved());
                changeCursorTexture(content, true);
            }
            else if (inputHandler.keyReleased(Keys.Space))
                changeCursorTexture(content, false);

            CursorPosition = Vector2.Transform(inputHandler.mousePosition(), editorCamera.getInverseTransform());

            updateHudPosition();

            if (mode == Modes.Edit)
            {
                /* Editing tilemap */
                if (clickOnHud(inputHandler.mousePosition()))
                {
                    #region Choosing tiles and other HUD stuff

                    #endregion
                }
                else
                {
                    #region Placing and removing tiles
                    // Adding tiles:
                    if (inputHandler.leftClicked())
                    {
                        Vector2 tilePosition = inputHandler.mousePosition();
                        tilePosition = Vector2.Transform(tilePosition, editorCamera.getInverseTransform());

                        float overX = tilePosition.X % 64;
                        float overY = tilePosition.Y % 64;

                        tilePosition.X -= overX;
                        tilePosition.Y -= overY;

                        currentLevel.placeTile(content.Load<Texture2D>("Env\\Dev\\devOrange"), tilePosition, false, false, 0, Layer);
                    }

                    // Removing tiles:
                    if (inputHandler.rightClicked())
                    {
                        Vector2 tilePosition = inputHandler.mousePosition();
                        tilePosition = Vector2.Transform(tilePosition, editorCamera.getInverseTransform());

                        float overX = tilePosition.X % 64;
                        float overY = tilePosition.Y % 64;

                        tilePosition.X -= overX;
                        tilePosition.Y -= overY;

                        currentLevel.removeTileCheck(tilePosition, Layer);
                    }
                    #endregion
                }
            }
        }

        // This method checks if a mouseclick happened inside of a hud element
        //  and returns true or false.
        private bool clickOnHud(Vector2 position)
        {
            bool hudClick = false;

            if (((position.X > (editorCamera.ViewportWidth - (RightBox.Width + (Margin * 2)))) 
                && position.Y < editorCamera.ViewportHeight)
                || (position.Y < (TopBox.Height + (Margin * 2))))
                hudClick = true;

            return hudClick;
        }

        // Changes the mode in the editor
        private void changeMode()
        {
            if (mode == Modes.Edit){
                ModeBox.setHeaderText("View Mode");
                mode = Modes.View;
            }
            else if (mode == Modes.View)
            {
                ModeBox.setHeaderText("Edit Mode");
                mode = Modes.Edit;
            }
        }

        private void changeCursorTexture(ContentManager content, bool moving)
        {
            if (moving)
                Cursor = content.Load<Texture2D>("Editor\\cursorMoveCam");
            else
                Cursor = content.Load<Texture2D>("Editor\\cursorNormal");
        }

        private void layerChecker(InputHandler inputHandler)
        {
            if (inputHandler.scrollwheelMoved() < 0)
                Layer--;
            else if (inputHandler.scrollwheelMoved() > 0)
                Layer++;

            if (Layer < 1)
                Layer = 1;
            else if (Layer > 3)
                Layer = 3;
        }

        private void updateHudPosition()
        {
            TopBox.update(new Vector2(  editorCamera.Position.X - ((editorCamera.ViewportWidth / 2) - Margin), 
                                        editorCamera.Position.Y - ((editorCamera.ViewportHeight / 2) - Margin)), Margin);
            
            RightBox.update(new Vector2 (editorCamera.Position.X + ((editorCamera.ViewportWidth / 2) - (RightBox.Width + Margin)), 
                                        (editorCamera.Position.Y - ((editorCamera.ViewportHeight / 2) - ((Margin * 2) + TopBox.Height)))), Margin);

            ModeBox.update(new Vector2( editorCamera.Position.X - ((editorCamera.viewportWidth / 2) - Margin), 
                                        editorCamera.Position.Y + ((editorCamera.ViewportHeight / 2) - ((Margin * 2) + modeBox.Height))), Margin);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            float opaque = 0.5f;
            float solid = 1.0f;

            if (mode == Modes.Edit)
            {
                #region Draw with opacity
                if (Layer == 1)
                {
                    currentLevel.drawLayerOne(spriteBatch, solid);
                    currentLevel.drawLayerTwo(spriteBatch, opaque);
                    currentLevel.drawLayerThree(spriteBatch, opaque);
                    currentLevel.drawGrid(spriteBatch, solid);
                }
                else if (Layer == 2)
                {
                    currentLevel.drawLayerOne(spriteBatch, opaque);
                    currentLevel.drawLayerTwo(spriteBatch, solid);
                    currentLevel.drawLayerThree(spriteBatch, opaque);
                    currentLevel.drawGrid(spriteBatch, solid);
                }
                else if (Layer == 3)
                {
                    currentLevel.drawLayerOne(spriteBatch, opaque);
                    currentLevel.drawLayerTwo(spriteBatch, opaque);
                    currentLevel.drawLayerThree(spriteBatch, solid);
                    currentLevel.drawGrid(spriteBatch, solid);
                }
                #endregion
            }
            else
            {
                #region Draw normally
                currentLevel.drawLayerOne(spriteBatch, solid);
                currentLevel.drawLayerTwo(spriteBatch, solid);
                currentLevel.drawLayerThree(spriteBatch, solid);
                currentLevel.drawGrid(spriteBatch, solid);
                #endregion
            }

            topBox.draw(spriteBatch);
            RightBox.draw(spriteBatch);
            ModeBox.draw(spriteBatch);

            spriteBatch.Draw(Cursor, CursorPosition, Color.White);
        }
        #endregion
    }
}
