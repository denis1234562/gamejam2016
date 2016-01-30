using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2016.Objects;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;

namespace GameJam2016.Scenes
{
    public class GameLevel : IScene
    {
        Player player = new Player();

        SpriteBatch spriteBatch;
        public static float speed = 5f;

        private ParallaxBackground background = new BackgroundEarth();
        private TileMap map;
        private Random random = new Random(DateTime.Now.Second);

        public float jumpingHeight = 0;
        public float jumpingHeightStart = -14;
        public bool jumping = false;

        public GameLevel()
        {

        }

        public void LoadContent(MyGame game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            map = new TileMap("Content/Maps/level1.txt");

            player.LoadContent(game);
            map.LoadContent(game);
            background.LoadContent(game);
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
            var action = ReadPlayerControls();

            if (!jumping)
            {
                if ((action & PlayerAction.Jump) == PlayerAction.Jump)
                {
                    jumping = true;
                    jumpingHeight = jumpingHeightStart;
                    player.PlayJumpSoundEffect();
                }
            }

            var limitY = map.GetFloorY(game, player.PlayerSize);
            var rightX = map.GetRightX(game, player.PlayerSize);
            var leftX = map.GetLeftX(game, player.PlayerSize);

            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", leftX, rightX));

            if ((action & PlayerAction.MoveLeft) == PlayerAction.MoveLeft && player.PlayerLocation.X < leftX)
            {
                action &= ~PlayerAction.MoveLeft;
            }

            if ((action & PlayerAction.MoveRight) == PlayerAction.MoveRight && player.PlayerLocation.X + player.PlayerSize.Width > rightX)
            {
                action &= ~PlayerAction.MoveRight;
            }

            if (jumping)
            {
                player.PlayerLocation.Y += jumpingHeight;
                jumpingHeight += .5f;

                if (jumpingHeight > -jumpingHeightStart)
                {
                    jumping = false;
                }

                if (player.PlayerLocation.Y + player.PlayerSize.Height > limitY)
                {
                    player.PlayerLocation.Y = limitY - player.PlayerSize.Height;
                    jumping = false;
                }
            }
            else
            {
                if (player.PlayerLocation.Y + player.PlayerSize.Height + 15 < limitY)
                {
                    player.PlayerLocation.Y += 15;
                }
                else
                {
                    player.PlayerLocation.Y = limitY - player.PlayerSize.Height;
                    jumping = false;
                }
            }

            player.Update(game, gameTime, action);
            background.Update(game, gameTime, action);
            map.Update(game, gameTime, action);
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            background.Draw(game, gameTime);
            player.Draw(game, gameTime);
            map.Draw(game, gameTime);
        }
    }
}
