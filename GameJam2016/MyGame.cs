using System;
using GameJam2016.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJam2016
{
    public class MyGame : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public IScene currentScene = null;

        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var scene = new GameLevel();
            scene.LoadContent(this);
            currentScene = scene;
        }

        protected override void UnloadContent()
        {
            if (currentScene != null)
            {
                currentScene.UnloadContent(this);
                currentScene = null;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
  
            if (currentScene != null)
            {
                currentScene.Update(this, gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (currentScene != null)
            {
                currentScene.Draw(this, gameTime);
            }

            base.Draw(gameTime);
        }

        public void setScene(IScene scene)
        {
            var oldScene = currentScene;

            // some day might do loading indicator... now just display empty screen
            currentScene = null;
            oldScene.UnloadContent(this);

            scene.LoadContent(this);
            currentScene = scene;
        }
    }
}
