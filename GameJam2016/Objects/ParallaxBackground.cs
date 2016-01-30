using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJam2016.Objects
{
    class ParallaxBackground : IScene
    {
        private List<Background> backgrounds;

        public void LoadContent(MyGame game)
        {
            //Load the background images
            backgrounds = new List<Background>();
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_07_1920 x 1080"), new Vector2(0, 0), 0.7f));
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_06_1920 x 1080"), new Vector2(10, 0), 0.9f));
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_05_1920 x 1080"), new Vector2(20, 0), 0.9f));
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_04_1920 x 1080"), new Vector2(50, 0), 0.9f));
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_03_1920 x 1080"), new Vector2(100, 0), 0.9f));
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_02_1920 x 1080"), new Vector2(290, 0), 1f));
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_01_1920 x 1080"), new Vector2(300, 0), 0.7f));

            // hack to position the background propery
            backgrounds[5].Offset.Y = 120;
            backgrounds[6].Offset.Y = 100;
        }

        public void UnloadContent(MyGame myGame)
        {

        }

        public void Update(MyGame game, GameTime gameTime, PlayerAction action)
        {
            //Get directional vector based on input
            Vector2 direction = Vector2.Zero;

            if ((action & PlayerAction.Jump) == PlayerAction.Jump)
            {
                direction = new Vector2(0, -1);
            }
            if ((action & PlayerAction.MoveLeft) == PlayerAction.MoveLeft)
            {
                direction += new Vector2(-1, 0);
            }
            else if ((action & PlayerAction.MoveRight) == PlayerAction.MoveRight)
            {
                direction += new Vector2(1, 0);
            }

            foreach (Background bg in backgrounds)
            {
                bg.Update(gameTime, direction, game.GraphicsDevice.Viewport);
            }
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            var spriteBatch = game.spriteBatch;

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

            foreach (Background bg in backgrounds)
            {
                bg.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        public void Update(MyGame game, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
