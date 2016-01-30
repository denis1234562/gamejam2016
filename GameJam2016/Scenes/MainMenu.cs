using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2016.Objects;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;

namespace GameJam2016.Scenes
{
    class MainMenu : IScene
    {
        SpriteBatch spriteBatch;
        Texture2D platform;
        Vector2 platform1Location;

        private ParallaxBackground background = new ParallaxBackground();

        public MainMenu()
        {
            platform1Location = new Vector2(400, 450);
        }

        public void LoadContent(MyGame game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            platform = game.Content.Load<Texture2D>("FireButton");

            background.LoadContent(game);
        }

        public void UnloadContent(MyGame game)
        {
            background.UnloadContent(game);
        }

        public void Update(MyGame game, GameTime gameTime)
        {

        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            background.Draw(game, gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(platform, new Vector2(platform1Location.X, platform1Location.Y));
            spriteBatch.Draw(platform, new Vector2(platform1Location.X + platform.Width, platform1Location.Y));
            spriteBatch.End();
        }
    }
}
