#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SideSouler_v2.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SideSouler_v2.Systems;
#endregion

namespace SideSouler_v2.Level
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
        private DialogBox coordBox;
        private InfoText tileText;
        private DialogBox test;

        private Texture2D cursorCurrent;
        private Texture2D cursorMove;
        private Texture2D cursorNormal;
        private Texture2D hoverRect;
        private Vector2 cursorPosition;
        private Vector2 cursorTilePosition;

        private Level currentLevel;

        private TileSheet tilesheet;
        private Texture2D selectionRect;
        private Vector2 selectionRectPosition;
        private Vector2 tileSelect;
        private Tile selectedTile;

        public EditorCamera editorCamera;

        private bool helpDiag;

        // TODO:
        //  Add other modes (Object Mode etc)
        private enum Modes
        {
            Edit,
            View,
            Help
        }

        private Modes mode;
        #endregion

        #region Constructor
        public Editor(Viewport view, ContentManager content, int screenWidth, int screenHeight)
        {
            mode = Modes.View;

            currentLevel = new Level(new Vector2(100, 100), "");
            currentLevel.populateWithGrid(content);

            Width = 32 * 100;
            Height = 32 * 100;

            EditorCamera = new EditorCamera(view, Width, Height, 1.0f);

            Margin = 2;     // MAGIC NUMBERS YAY
            Layer = 2;

            initDialogBoxes(content, screenWidth, screenHeight);

            CursorPosition = Vector2.Zero;

            TileText = new InfoText(content, "Tilesheet", new Vector2(370, -315));  // again, MAGIC NUMBERS!

            SelectedTile = null;

            HelpDiag = false;
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

        private DialogBox CoordBox
        {
            get { return this.coordBox; }
            set { this.coordBox = value; }
        }

        private InfoText TileText
        {
            get { return this.tileText; }
            set { this.tileText = value; }
        }

        public Texture2D HoverRect
        {
            get { return this.hoverRect; }
            set { this.hoverRect = value; }
        }

        public Texture2D CursorCurrent
        {
            get { return this.cursorCurrent; }
            set { this.cursorCurrent = value; }
        }

        public Texture2D CursorMove
        {
            get { return this.cursorMove; }
            set { this.cursorMove = value; }
        }

        public Texture2D CursorNormal
        {
            get { return this.cursorNormal; }
            set { this.cursorNormal = value; }
        }

        public Vector2 CursorPosition
        {
            get { return this.cursorPosition; }
            set { this.cursorPosition = value; }
        }

        public Vector2 CursorTilePosition
        {
            get { return this.cursorTilePosition; }
            set { this.cursorTilePosition = value; }
        }

        public TileSheet Tilesheet
        {
            get { return this.tilesheet; }
            set { this.tilesheet = value; }
        }

        public Texture2D SelectionRect
        {
            get { return this.selectionRect; }
            set { this.selectionRect = value; }
        }

        public Vector2 SelectionRectPosition
        {
            get { return this.selectionRectPosition; }
            set { this.selectionRectPosition = value; }
        }

        private Vector2 TileSelect
        {
            get { return this.tileSelect; }
            set { this.tileSelect = value; }
        }

        private Tile SelectedTile
        {
            get { return this.selectedTile; }
            set { this.selectedTile = value; }
        }

        public EditorCamera EditorCamera
        {
            get { return this.editorCamera; }
            set { this.editorCamera = value; }
        }

        private bool HelpDiag
        {
            get { return this.helpDiag; }
            set { this.helpDiag = value; }
        }
        #endregion

        #region Methods
        public void LoadContent(ContentManager content)
        {
            CursorMove = content.Load<Texture2D>("Editor\\cursorMoveCam");
            CursorNormal = content.Load<Texture2D>("Editor\\cursorNormal");
            CursorCurrent = CursorNormal;

            HoverRect = content.Load<Texture2D>("Editor\\hoverRect");

            Tilesheet = new TileSheet(content.Load<Texture2D>("Env\\tilesheet01"), 32, new Vector2(370, -290));
            SelectionRect = content.Load<Texture2D>("Editor\\selectionRect");
            SelectionRectPosition = new Vector2(370, -290);
            TileSelect = Tilesheet.Position;
        }

        public void initDialogBoxes(ContentManager content, int screenWidth, int screenHeight)
        {
            ModeBox = new DialogBox(content, new Vector2(screenWidth / 2 - 100, screenHeight - 20), 128, 24, Margin);
            ModeBox.setHeaderText("View Mode");

            TopBox = new DialogBox(content, new Vector2(Margin, Margin), screenWidth - (Margin * 2), 25, Margin);
            TopBox.setHeaderText("Editor   -   Layer: " + Layer);

            RightBox = new DialogBox(content, new Vector2(screenWidth - 230, 30), 286, 687, Margin);

            CoordBox = new DialogBox(content, Vector2.Zero, 128, 24, Margin);
            CoordBox.setHeaderText("X: Y:");

            test = new DialogBox(content, 450, 250, 100, 123, Margin);
            test.addText("                      HELP");
            test.addText("");
            test.addText("  All Modes");
            test.addText("Switch modes - Tab");
            test.addText("Move camera - Hold space");
            test.addText("Change layers - Scrollwheel");
            test.addText("");
            test.addText("  Edit Mode");
            test.addText("Add tiles - Left mousebutton");
            test.addText("Remove tiles - Right mousebutton");
        }

        public void update(ContentManager content, InputHandler inputHandler)
        {
            /* Changing modes */
            if (inputHandler.keyReleased(Keys.Tab))
                changeMode(false);
            if (inputHandler.keyReleased(Keys.F1))
                changeMode(true);

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

            CoordBox.setHeaderText("X: " + CursorPosition.X + " Y: " + CursorPosition.Y);

            updateHudPosition();

            switch (mode)
            {
                case Modes.Edit:
                    CursorTilePosition = inputHandler.mousePosition();
                    CursorTilePosition = Vector2.Transform(CursorTilePosition, editorCamera.getInverseTransform());

                    float overX = CursorTilePosition.X % 32;
                    float overY = CursorTilePosition.Y % 32;

                    CursorTilePosition -= new Vector2(overX, overY);

                    /* Editing tilemap */
                    if (clickOnHud(inputHandler.mousePosition()))
                    {
                        #region Choosing tiles and other HUD stuff
                        if (inputHandler.leftClicked())
                        {
                            // The tilesheet is located at (1010, 70).
                            //  ((640 + 370), (360 - 290))
                            //  THERE ARE WAY TOO MANY NUMBERS HERE. 
                            //  GET THIS SHIT IN ORDER
                            if (((inputHandler.mousePosition().X > 1010) && (inputHandler.mousePosition().X < (1010 + Tilesheet.SheetWidth)))
                                && ((inputHandler.mousePosition().Y > 70) && (inputHandler.mousePosition().Y < (70 + Tilesheet.SheetHeight))))
                            {
                                tileSelect = new Vector2(((inputHandler.mousePosition().X - ((inputHandler.mousePosition().X - 1010) % 32)) - 1010),
                                                                    ((inputHandler.mousePosition().Y - ((inputHandler.mousePosition().Y - 70) % 32)) - 70));

                                SelectionRectPosition = Tilesheet.Position + tileSelect;

                                SelectedTile = Tilesheet.getTile(((int)tileSelect.X / Tilesheet.TileSize), ((int)tileSelect.Y / Tilesheet.TileSize));
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Placing and removing tiles
                        // Adding tiles:
                        if (inputHandler.leftClicked())
                        {
                            if (SelectedTile != null)
                            {
                                SelectedTile.setPosition(CursorTilePosition);
                                currentLevel.placeTile(SelectedTile, CursorTilePosition, Layer);

                                SelectedTile = null;
                            }

                            if (SelectedTile == null)
                            {
                                SelectedTile = Tilesheet.getTile(((int)tileSelect.X / Tilesheet.TileSize), ((int)tileSelect.Y / Tilesheet.TileSize));
                            }
                        }

                        // Removing tiles:
                        if (inputHandler.rightClicked())
                        {
                            currentLevel.removeTileCheck(CursorTilePosition, Layer);
                        }
                        #endregion
                    }
                    break;
                case Modes.View:
                    break;
                case Modes.Help:
                    break;
            }
            if (mode == Modes.Edit)
            {

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
        private void changeMode(bool help)
        {
            if (!help)
            {
                if (mode == Modes.Edit)
                {
                    ModeBox.setHeaderText("View Mode");
                    mode = Modes.View;
                }
                else if (mode == Modes.View)
                {
                    ModeBox.setHeaderText("Edit Mode");
                    mode = Modes.Edit;
                }
            }
            else
            {
                if (HelpDiag)
                    HelpDiag = false;
                else
                    HelpDiag = true;
            }
        }

        private void changeCursorTexture(ContentManager content, bool moving)
        {
            if (moving)
                CursorCurrent = CursorMove;
            else
                CursorCurrent = CursorNormal;
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
            TopBox.update(new Vector2(editorCamera.Position.X - ((editorCamera.ViewportWidth / 2) - Margin),
                                        editorCamera.Position.Y - ((editorCamera.ViewportHeight / 2) - Margin)), Margin);

            RightBox.update(new Vector2(editorCamera.Position.X + ((editorCamera.ViewportWidth / 2) - (RightBox.Width + Margin)),
                                        (editorCamera.Position.Y - ((editorCamera.ViewportHeight / 2) - ((Margin * 2) + TopBox.Height)))), Margin);

            ModeBox.update(new Vector2(editorCamera.Position.X - ((editorCamera.viewportWidth / 2) - Margin),
                                        editorCamera.Position.Y + ((editorCamera.ViewportHeight / 2) - ((Margin * 2) + modeBox.Height))), Margin);

            CoordBox.update(new Vector2(RightBox.Position.X - ((Margin * 2) + (CoordBox.Width)),
                                        editorCamera.Position.Y + ((editorCamera.ViewportHeight / 2) - (CoordBox.Height + (Margin * 2)))), Margin);

            TileText.update(new Vector2(editorCamera.Position.X, editorCamera.Position.Y));

            Tilesheet.update(new Vector2(editorCamera.Position.X, editorCamera.Position.Y));

            SelectionRectPosition = Tilesheet.Position + TileSelect;

            if (HelpDiag)
                test.update(editorCamera.Position, Margin);
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

                spriteBatch.Draw(HoverRect, CursorTilePosition, Color.White * opaque);
            }
            else
            {
                #region Draw normally
                currentLevel.drawLayerOne(spriteBatch, solid);
                currentLevel.drawLayerTwo(spriteBatch, solid);
                currentLevel.drawLayerThree(spriteBatch, solid);
                #endregion
            }

            topBox.draw(spriteBatch);
            RightBox.draw(spriteBatch);
            ModeBox.draw(spriteBatch);
            CoordBox.draw(spriteBatch);

            TileText.draw(spriteBatch);
            Tilesheet.draw(spriteBatch);

            spriteBatch.Draw(SelectionRect, SelectionRectPosition, Color.White);

            if (HelpDiag)
                test.draw(spriteBatch);

            spriteBatch.Draw(CursorCurrent, CursorPosition, Color.White);
        }
        #endregion
    }
}
