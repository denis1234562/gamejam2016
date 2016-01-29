using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2016.Objects;
using Microsoft.Xna.Framework.Input;

namespace GameJam2016.Scenes
{
    class GameLevel : IScene
    {
        private ParallaxBackground background = new ParallaxBackground();

        public void LoadContent(MyGame game)
        {
            background.LoadContent(game);
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

            background.Update(game, gameTime, action);
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            background.Draw(game, gameTime);
        }
    }
}
