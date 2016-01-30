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

        public Vector2 powerAccessorLocation;
        public Texture2D powerAccessor;

        static Texture2D platform;
        public static Vector2 platform1Location;

        public Vector2[] PowerLocations;
        public Texture2D[,] PowerTextures;

        private ParallaxBackground background = new BackgroundFire();
        private TileMap map;
        private Random random = new Random(DateTime.Now.Second);

        public static float speed = 5f;
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

            List<int> X = new List<int> { 933, 1135, 1027, 1030 };
            List<int> Y = new List<int> { 407, 437, 335, 515 };
            for (int i = 0; i < returnLocations.Length; i++)
            {
                returnLocations[i] = new Vector2(X[i], Y[i]);
            }
            return returnLocations;
        }


        public GameLevel()
        {
            powerAccessorLocation = new Vector2(900, 350);
            platform1Location = new Vector2(400, 450);
        }

        public void LoadContent(MyGame game)
        {
            PowerTextures = LoadPowerTextures(game);
            PowerLocations = LoadPowerLocations();
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            map = new TileMap("Content/Maps/level1.txt");
            powerAccessor = game.Content.Load<Texture2D>("Powers/powers.gif");
            platform = game.Content.Load<Texture2D>("box");

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

            player.Update(game, gameTime, action);
            background.Update(game, gameTime, action);
            map.Update(game, gameTime, action);
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            background.Draw(game, gameTime);
            player.Draw(game, gameTime);
            map.Draw(game, gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(PowerTextures[0, 0], PowerLocations[0], null, Color.White, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);//fire
            spriteBatch.Draw(PowerTextures[1, 0], PowerLocations[1], null, Color.White, 0, Vector2.Zero, .58f, SpriteEffects.None, 0);//water
            spriteBatch.Draw(PowerTextures[2, 0], PowerLocations[2], null, Color.White, 0, Vector2.Zero, .68f, SpriteEffects.None, 0);//earth
            spriteBatch.Draw(PowerTextures[3, 0], PowerLocations[3], null, Color.White, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);//air

            spriteBatch.Draw(platform, platform1Location);
            spriteBatch.Draw(platform, new Vector2(platform1Location.X + platform.Width, platform1Location.Y));
            spriteBatch.Draw(powerAccessor, powerAccessorLocation);

            spriteBatch.End();
        }
    }
}
