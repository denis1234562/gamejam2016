﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2016.Objects;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;

namespace GameJam2016.Scenes
{
    public class MainMenu : IScene
    {
        int timer = 0;
        bool[] selectedButton = new bool[4];
        int selectedIndex = 0;
        SpriteBatch spriteBatch;
        Texture2D fireButton;
        Texture2D earthButton;
        Texture2D airButton;
        Texture2D waterButton;
        Texture2D fireButtonSelected;
        Texture2D earthButtonSelected;
        Texture2D airButtonSelected;
        Texture2D waterButtonSelected;
        Texture2D ritual;
        Texture2D pentagram;

        private ParallaxBackground background = new ParallaxBackground();

        public MainMenu()
        {
        }

        public void LoadContent(MyGame game)
        {
            selectedButton[0] = true;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            fireButton = game.Content.Load<Texture2D>("FireButton");
            earthButton = game.Content.Load<Texture2D>("EarthButton");
            airButton = game.Content.Load<Texture2D>("AirButton");
            waterButton = game.Content.Load<Texture2D>("WaterButton");
            fireButtonSelected = game.Content.Load<Texture2D>("FireButtonSelected");
            earthButtonSelected = game.Content.Load<Texture2D>("EarthButtonSelected");
            airButtonSelected = game.Content.Load<Texture2D>("AirButtonSelected");
            waterButtonSelected = game.Content.Load<Texture2D>("WaterButtonSelected");
            ritual = game.Content.Load<Texture2D>("ritual");
            pentagram = game.Content.Load<Texture2D>("pentagram");

            background.LoadContent(game);
        }

        public override void LoadContent(MyGame game)
        {
            backgrounds = new List<Background>();

            var fireTextures = new Texture2D[] {
                game.Content.Load<Texture2D>(@"Background\Earth\1"),
                game.Content.Load<Texture2D>(@"Background\Earth\2"),
                game.Content.Load<Texture2D>(@"Background\Earth\3"),
                game.Content.Load<Texture2D>(@"Background\Earth\4"),
                game.Content.Load<Texture2D>(@"Background\Earth\5"),
                game.Content.Load<Texture2D>(@"Background\Earth\6"),
                game.Content.Load<Texture2D>(@"Background\Earth\7"),
                game.Content.Load<Texture2D>(@"Background\Earth\8"),
            };
            backgrounds.Add(new BackgroundAnimated(fireTextures, new Vector2(0, 0), 2f, 4f));

            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_03_1920 x 1080"), new Vector2(100, 0), 0.9f));
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_02_1920 x 1080"), new Vector2(300, 0), 1f));
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_01_1920 x 1080"), new Vector2(300, 0), 0.7f));

            // hack to position the background propery
            backgrounds[backgrounds.Count - 2].Offset.Y = 120;
            backgrounds[backgrounds.Count - 1].Offset.Y = 100;
        }

        public void UnloadContent(MyGame game)
        {
            background.UnloadContent(game);
        }

        public void Update(MyGame game, GameTime gameTime)
        {
            timer++;
            var kbState = Keyboard.GetState();
            var gpState = GamePad.GetState(PlayerIndex.One);

            if (timer > 0)
            {
                if ((kbState.IsKeyDown(Keys.Right)
                                || kbState.IsKeyDown(Keys.D)
                                || gpState.IsButtonDown(Buttons.DPadRight)
                                || gpState.IsButtonDown(Buttons.LeftThumbstickRight)))
                {
                    timer = -25;
                    selectedButton[selectedIndex] = false;
                    selectedButton[(selectedIndex + 1) % 4] = true;
                    selectedIndex = (selectedIndex + 1) % 4;
                    this.Draw(game, gameTime);
                }

                if (kbState.IsKeyDown(Keys.Left)
                    || kbState.IsKeyDown(Keys.A)
                    || gpState.IsButtonDown(Buttons.DPadLeft)
                    || gpState.IsButtonDown(Buttons.LeftThumbstickLeft))
                {
                    timer = -25;
                    selectedButton[selectedIndex] = false;
                    selectedButton[(selectedIndex + 3) % 4] = true;
                    selectedIndex = (selectedIndex + 3) % 4;
                    this.Draw(game, gameTime);
                }
            }
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            background.Draw(game, gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(pentagram, new Vector2(250, 0));

            if (selectedButton[0])
            {
                spriteBatch.Draw(fireButtonSelected, new Vector2(250, 200));
            }
            else
            {
                spriteBatch.Draw(fireButton, new Vector2(250, 200));
            }

            if (selectedButton[1])
            {
                spriteBatch.Draw(earthButtonSelected, new Vector2(750, 500));
            }
            else
            {
                spriteBatch.Draw(earthButton, new Vector2(750, 500));
            }

            if (selectedButton[2])
            {
                spriteBatch.Draw(airButtonSelected, new Vector2(350, 500));
            }
            else
            {
                spriteBatch.Draw(airButton, new Vector2(350, 500));
            }

            if (selectedButton[3])
            {
                spriteBatch.Draw(waterButtonSelected, new Vector2(850, 200));
            }
            else
            {
                spriteBatch.Draw(waterButton, new Vector2(850, 200));
            }
            spriteBatch.Draw(ritual, new Vector2(525, 0));
            spriteBatch.End();
        }
    }
}
