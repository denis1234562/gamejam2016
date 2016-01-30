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
        static Texture2D platform;
        public static Vector2 platform1Location;
        public static float speed = 5f;

        private ParallaxBackground background = new BackgroundEarth();
        private TileMap map;
        private Random random = new Random(DateTime.Now.Second);

        public GameLevel()
        {
            platform1Location = new Vector2(400, 450);
        }

        public void LoadContent(MyGame game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            map = new TileMap("Content/Maps/level1.txt");
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
            spriteBatch.Draw(platform, new Vector2(platform1Location.X, platform1Location.Y));
            spriteBatch.Draw(platform, new Vector2(platform1Location.X + platform.Width, platform1Location.Y));
            spriteBatch.End();
        }
    }
}
