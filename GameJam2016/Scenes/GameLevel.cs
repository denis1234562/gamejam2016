using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2016.Objects;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameJam2016.Scenes
{
    public class GameLevel : IScene
    {
        Player player = new Player();

        SpriteBatch spriteBatch;
        public static float speed = 5f;
        public float jumpingHeight = 0;
        public float jumpingHeightStart = -14;
        public bool jumping = false;

        public static Vector2[] PowerLocations;
        public static Texture2D[,] PowerTextures;

        private ParallaxBackground background = new BackgroundFire();
        private TileMap map;
        private Random random = new Random(DateTime.Now.Second);

        public static Texture2D [,] LoadPowerTextures (MyGame game)
        {
            int amountOfPowersRows = 4, amountOfPowerCollumns = 2;
            Texture2D[,] returnTextures = new Texture2D[amountOfPowersRows, amountOfPowerCollumns];
            List<string> NamesNonClicked = new List<string> { "Powers/FireButton", "Powers/WaterButton", "Powers/EarthButton", "Powers/AirButton" };
            List<string> NamesClicked = new List<string> { "Powers/FireButtonClicked", "Powers/WaterButtonClicked", "Powers/EarthButtonClicked", "Powers/AirButtonClicked" };
            for (int i = 0; i < NamesNonClicked.Count; i++)
            {
                returnTextures[i, 0] = game.Content.Load<Texture2D>(NamesNonClicked[i]);
                returnTextures[i, 1] = game.Content.Load<Texture2D>(NamesClicked[i]);
            }
            return returnTextures;
        }
        public static Vector2 [] LoadPowerLocations()
        {
            Vector2[] returnLocations = new Vector2 [4];

            List<int> X = new List<int> { 933, 1027, 1135, 1030 };
            List<int> Y = new List<int> { 407, 335 , 437, 515 };
            for (int i = 0; i < returnLocations.Length; i++)
            {
                returnLocations[i] = new Vector2(X[i], Y[i]);
            }
            return returnLocations;
        }

        public GameLevel()
        {

        }

        public void LoadContent(MyGame game)
        {
            PowerTextures = LoadPowerTextures(game);
            PowerLocations = LoadPowerLocations();
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
                || gpState.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                action |= PlayerAction.Jump;
            }

            if (kbState.IsKeyDown(Keys.Left)
                || kbState.IsKeyDown(Keys.A)
                || gpState.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                action |= PlayerAction.MoveLeft;
            }
            else if (kbState.IsKeyDown(Keys.Right)
                || kbState.IsKeyDown(Keys.D)
                || gpState.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                action |= PlayerAction.MoveRight;
            }

            if (kbState.IsKeyDown(Keys.Space)
                || gpState.IsButtonDown(Buttons.A))
            {
                action |= PlayerAction.Shoot;
                if(player.currentPower==0)
                {

                }
            }

            if (kbState.IsKeyDown(Keys.NumPad1)
                || kbState.IsKeyDown(Keys.D1)
                || gpState.IsButtonDown(Buttons.DPadLeft))
            {
                action |= PlayerAction.Fire;
            }
            if (kbState.IsKeyDown(Keys.NumPad2)
                || kbState.IsKeyDown(Keys.D2)
                || gpState.IsButtonDown(Buttons.DPadUp))
            {
                action |= PlayerAction.Earth;
            }
            if (kbState.IsKeyDown(Keys.NumPad3)
                || kbState.IsKeyDown(Keys.D3)
                || gpState.IsButtonDown(Buttons.DPadRight))
            {
                action |= PlayerAction.Water;
            }
            if (kbState.IsKeyDown(Keys.NumPad4)
                || kbState.IsKeyDown(Keys.D4)
                || gpState.IsButtonDown(Buttons.DPadDown))
            {
                action |= PlayerAction.Air;
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
            //game.spriteBatch.Draw(PowerTextures[currentPower, 1], GameLevel.PowerLocations[currentPower], null, Color.White, 0, Vector2.Zero, currentPowerImageScale, SpriteEffects.None, 0);
            background.Draw(game, gameTime);
            map.Draw(game, gameTime);
            player.Draw(game, gameTime);
        }
    }
}
