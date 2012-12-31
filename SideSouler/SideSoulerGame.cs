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

namespace SideSouler
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

        public SideSoulerGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            // Window settings
            Window.Title = gameName;
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }

        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = false;

            editor = new Level.Editor(GraphicsDevice.Viewport, Content, width, height);
            inputHandler = new Systems.InputHandler();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            inputHandler.update();
            editor.update(Content, inputHandler);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
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
