using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2016.Objects;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;

namespace GameJam2016.Scenes
{
    class GameLevel : IScene
    {
        SpriteBatch spriteBatch;
        Texture2D platform;
        Vector2 platform1Location;

        private ParallaxBackground background = new ParallaxBackground();
        private AnimatedSprite animatedSprite;
        private SoundEffect soundEffectJump;

        private Random random = new Random(DateTime.Now.Second);
        private bool right = false;
        private bool left = false;
        private float startX = 200;
        private float startY = 550;
        private Vector2 heroLocation;
        private bool jumping;
        private float jumpspeed = 0,jumpStart = -14f;

        public GameLevel()
        {
            platform1Location = new Vector2(400, 450);
            heroLocation = new Vector2(startX, startY);
            startY = heroLocation.Y;
            jumping = false;
            jumpspeed = 0;
        }

        public void LoadContent(MyGame game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            platform = game.Content.Load<Texture2D>("box");
            soundEffectJump = game.Content.Load<SoundEffect>("Sounds/238282__meroleroman7__robot-jump-2");

            background.LoadContent(game);
            Texture2D texture = game.Content.Load<Texture2D>("linkEdit");
            animatedSprite = new AnimatedSprite(texture, 8, 10, new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2) });
        }

        public void UnloadContent(MyGame game)
        {
            background.UnloadContent(game);
        }

        public PlayerAction ReadPlayerControls()
        {
            var action = PlayerAction.None;
            var kbState = Keyboard.GetState();
            var gpState = GamePad.GetState(PlayerIndex.One);

            if (kbState.IsKeyDown(Keys.Up)
                || kbState.IsKeyDown(Keys.W)
                || gpState.IsButtonDown(Buttons.DPadUp)
                || gpState.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                action |= PlayerAction.Jump;
            }

            if (kbState.IsKeyDown(Keys.Left)
                || kbState.IsKeyDown(Keys.A)
                || gpState.IsButtonDown(Buttons.DPadLeft)
                || gpState.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                action |= PlayerAction.MoveLeft;
            }
            else if (kbState.IsKeyDown(Keys.Right)
                || kbState.IsKeyDown(Keys.D)
                || gpState.IsButtonDown(Buttons.DPadRight)
                || gpState.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                action |= PlayerAction.MoveRight;
            }

            if (kbState.IsKeyDown(Keys.Space)
                || gpState.IsButtonDown(Buttons.A))
            {
                action |= PlayerAction.Fire;
            }

            return action;
        }

        public void Update(MyGame game, GameTime gameTime)
        {
            var action = this.ReadPlayerControls();
            Background getFromBG = new Background(platform, new Vector2(5, 0), 0);
            var speed = getFromBG.Speed.X;

            if ((action & PlayerAction.MoveRight) == PlayerAction.MoveRight)
            {
                int row = 7;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2),
                                                               new Vector2(row, 2),new Vector2(row, 3),new Vector2(row, 4),
                                                               new Vector2(row, 5),new Vector2(row, 6),new Vector2(row, 7) ,
                                                               new Vector2(row, 8), new Vector2(row , 9)};
                right = true;
                left = false;

                platform1Location.X -= speed;
            }
            else if ((action & PlayerAction.MoveLeft) == PlayerAction.MoveLeft)
            {
                int row = 5;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2),
                                                               new Vector2(row, 2),new Vector2(row, 3),new Vector2(row, 4),
                                                               new Vector2(row, 5),new Vector2(row, 6),new Vector2(row, 7) ,
                                                               new Vector2(row, 8), new Vector2(row , 9)};
                left = true;
                right = false;
                platform1Location.X += speed;
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
                if ((action & PlayerAction.Jump) == PlayerAction.Jump)
                {
                    jumping = true;
                    jumpspeed = jumpStart;
                    soundEffectJump.CreateInstance().Play();
                }
            }
            animatedSprite.Update(gameTime);
            background.Update(game, gameTime, action);
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            background.Draw(game, gameTime);
            animatedSprite.Draw(game.spriteBatch, heroLocation);

            spriteBatch.Begin();
            spriteBatch.Draw(platform, new Vector2(platform1Location.X, platform1Location.Y));
            spriteBatch.Draw(platform, new Vector2(platform1Location.X + platform.Width, platform1Location.Y));
            spriteBatch.End();
        }
    }
}
