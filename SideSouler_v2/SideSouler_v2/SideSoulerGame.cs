/************************************************************
 * SideSouler v2                                            *
 ************************************************************
 *  SideSoulerGame.cs                                       *
 * Core class that handles pretty much everything! :D       *
 ************************************************************
 * By Inge Dalby, 2014                                      *
 ************************************************************/

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace SideSouler_v2
{
    public class SideSoulerGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level.Editor editor;
        Systems.InputHandler inputHandler;

        // Window variables
        String gameName = "SideSouler";
        int width = 1280;
        int height = 720;

        // FPS and UPS
        private int updates;
        private int frames;
        private float elapsed;
        private float totalElapsed;

        public SideSoulerGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Window settings
            Window.Title = gameName;
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
        }

        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = false;
        }

        protected override void LoadContent()
        {
            editor = new Level.Editor(GraphicsDevice.Viewport, Content, width, height);
            inputHandler = new Systems.InputHandler();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            editor.LoadContent(Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (totalElapsed > 1)
            {
                Window.Title = gameName + " | " + frames + "FPS " + updates + "UPS ";
                totalElapsed = 0;
                updates = 0;
                frames = 0;
            }

            updates++;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            inputHandler.update();
            editor.update(Content, inputHandler);

            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totalElapsed += elapsed;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            frames++;

            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.LinearWrap,
                DepthStencilState.Default,
                RasterizerState.CullNone, null, editor.EditorCamera.getTransformation());
            editor.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
