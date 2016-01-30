using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2016.Objects;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameJam2016.Scenes
{
    class GameLevel : IScene
    {
        private ParallaxBackground background = new ParallaxBackground();
        private AnimatedSprite animatedSprite;
        private bool right = false;
        private bool left = false;
        private float startX = 200;
        private float startY = 550;
        private Vector2 heroLocation;
        bool jumping; 
        float jumpspeed = 0;



        public void LoadContent(MyGame game)
        {
            background.LoadContent(game);

            Texture2D texture = game.Content.Load<Texture2D>("linkEdit");
            animatedSprite = new AnimatedSprite(texture, 8, 10, new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2) });
        }

        public GameLevel()
        {
            heroLocation = new Vector2(startX, startY);
            startY = heroLocation.Y;
            jumping = false;
            jumpspeed = 0;
        }

        public void UnloadContent(MyGame game)
        {
            background.UnloadContent(game);
        }

        public void Update(MyGame game, GameTime gameTime)
        {
            var action = PlayerAction.None;

            var kbState = Keyboard.GetState();
            var gpState = GamePad.GetState(PlayerIndex.One);

            if (kbState.IsKeyDown(Keys.Up) || kbState.IsKeyDown(Keys.W) || gpState.IsButtonDown(Buttons.DPadUp))
            {
                action = PlayerAction.Jump;
            }
            if (kbState.IsKeyDown(Keys.Left) || kbState.IsKeyDown(Keys.A) || gpState.IsButtonDown(Buttons.DPadLeft))
            {
                action = PlayerAction.MoveLeft;
            }
            else if (kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.D) || gpState.IsButtonDown(Buttons.DPadRight))
            {
                action = PlayerAction.MoveRight;
            }

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightStick))
            {
                int row = 7;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2),
                                                               new Vector2(row, 2),new Vector2(row, 3),new Vector2(row, 4),
                                                               new Vector2(row, 5),new Vector2(row, 6),new Vector2(row, 7) ,
                                                               new Vector2(row, 8), new Vector2(row , 9)};
                right = true;
                left = false;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftStick))
            {
                int row = 5;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2),
                                                               new Vector2(row, 2),new Vector2(row, 3),new Vector2(row, 4),
                                                               new Vector2(row, 5),new Vector2(row, 6),new Vector2(row, 7) ,
                                                               new Vector2(row, 8), new Vector2(row , 9)};
                left = true;
                right = false;
            }
            else if (right)
            {

                right = false;
                int row = 3;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2) };
            }
            else if (left)
            {
                left = false;
                int row = 1;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2) };
            }
            if (jumping)
            {
                heroLocation.Y += jumpspeed;
                jumpspeed += 1;
                if (heroLocation.Y >= startY)
                {
                    heroLocation.Y = startY;
                    jumping = false;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    jumping = true;
                    jumpspeed = -14;
                }
            }
            animatedSprite.Update(gameTime);
            background.Update(game, gameTime, action);
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            background.Draw(game, gameTime);
            animatedSprite.Draw(game.spriteBatch, heroLocation);
        }
    }
}
