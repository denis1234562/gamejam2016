using GameJam2016.Objects.PlayerSpells;
using GameJam2016.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameJam2016
{
    public class Player : IScene
    {
        public Vector2 PlayerLocation;
        public Rectangle PlayerSize;

        private bool right, left;
        private AnimatedSprite animatedSprite;
        private SoundEffect soundEffectJump;
        public float startX = 200;
        public float startY = 300; //550;
        public static int currentPower = 4;
        private float currentPowerImageScale;
        private Texture2D currentTexture;

        static Texture2D textureBasic;
        static List<Vector2> textureBasicPosition = new List<Vector2>();

        static Texture2D textureFire;
        static List<Vector2> textureFirePosition = new List<Vector2>();

        static Texture2D textureWater;
        static List<Vector2> textureWaterPosition = new List<Vector2>();

        static Texture2D textureEarth;
        static List<Vector2> textureEarthPosition = new List<Vector2>();

        static Texture2D textureAir;
        static List<Vector2> textureAirPosition = new List<Vector2>();

        public Player()
        {
            PlayerLocation = new Vector2(startX, startY);
        }

        public void Update(MyGame game, GameTime gameTime, PlayerAction action)
        {
            if ((action & PlayerAction.MoveRight) == PlayerAction.MoveRight)
            {
                var newLocations = AddTexturesToList(2, 4);
                animatedSprite.Animation = newLocations.ToArray();
                right = true;
                left = false;
            }
            else if ((action & PlayerAction.MoveLeft) == PlayerAction.MoveLeft)
            {
                var newLocations = AddTexturesToList(1, 4);
                animatedSprite.Animation = newLocations.ToArray();
                left = true;
                right = false;
            }
            else if (right)
            {
                right = false;
                var newLocations = AddTexturesToList(2, 1);
                animatedSprite.Animation = newLocations.ToArray();
            }
            else if (left)
            {
                left = false;
                var newLocations = AddTexturesToList(1, 1);
                animatedSprite.Animation = newLocations.ToArray();
            }

            if ((action & PlayerAction.Fire) == PlayerAction.Fire)
            {
                currentPower = (int)Powers.Fire;
                currentPowerImageScale = .7f;
                animatedSprite.Texture = textureFire;
            }
            if ((action & PlayerAction.Earth) == PlayerAction.Earth)
            {
                currentPower = (int)Powers.Earth;
                currentPowerImageScale = .68f;
                animatedSprite.Texture = textureEarth;
            }
            if ((action & PlayerAction.Air) == PlayerAction.Air)
            {
                currentPower = (int)Powers.Air;
                currentPowerImageScale = .7f;
                animatedSprite.Texture = textureAir;
            }
            if ((action & PlayerAction.Water) == PlayerAction.Water)
            {
                currentPower = (int)Powers.Water;
                currentPowerImageScale = .58f;
                animatedSprite.Texture = textureWater;
            }
            animatedSprite.Update(gameTime);
            PlayerSize.X = (int)PlayerLocation.X;
            PlayerSize.Y = (int)PlayerLocation.Y;
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(GameLevel.PowerTextures[0, 0], GameLevel.PowerLocations[0], null, Color.White, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);//fire
            game.spriteBatch.Draw(GameLevel.PowerTextures[1, 0], GameLevel.PowerLocations[1], null, Color.White, 0, Vector2.Zero, .58f, SpriteEffects.None, 0);//water
            game.spriteBatch.Draw(GameLevel.PowerTextures[2, 0], GameLevel.PowerLocations[2], null, Color.White, 0, Vector2.Zero, .68f, SpriteEffects.None, 0);//earth
            game.spriteBatch.Draw(GameLevel.PowerTextures[3, 0], GameLevel.PowerLocations[3], null, Color.White, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);//air
            if (currentPower != 4)
            {
                game.spriteBatch.Draw(GameLevel.PowerTextures[currentPower, 1], GameLevel.PowerLocations[currentPower], null, Color.White, 0, Vector2.Zero, currentPowerImageScale, SpriteEffects.None, 0);
            }

            game.spriteBatch.End();
            animatedSprite.Draw(game.spriteBatch, PlayerLocation);
            
        }

        public void UnloadContent(MyGame myGame)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(MyGame myGame)
        {
            soundEffectJump = myGame.Content.Load<SoundEffect>("Sounds/238282__meroleroman7__robot-jump-2");
            textureBasic = myGame.Content.Load<Texture2D>("Powers/Basic/basic");
            textureFire = myGame.Content.Load<Texture2D>("Powers/Fire/fire");
            textureWater = myGame.Content.Load<Texture2D>("Powers/Water/water");
            textureEarth = myGame.Content.Load<Texture2D>("Powers/Earth/earth");
            textureAir = myGame.Content.Load<Texture2D>("Powers/Air/air");

            animatedSprite = new AnimatedSprite(textureBasic, 4, 4, new Vector2[] { new Vector2(0,0)});
            currentTexture = textureEarth;
            PlayerSize = new Rectangle(0, 0, currentTexture.Width / animatedSprite.Columns, currentTexture.Height / animatedSprite.Rows);
        }
        public static List<Vector2> AddTexturesToList(int row,int upToWhichFrame)
        {
            List<Vector2> texturePositionsInSprite = new List<Vector2>();
            textureBasicPosition.Clear();textureFirePosition.Clear();textureWaterPosition.Clear();textureAirPosition.Clear();textureEarthPosition.Clear();
            bool[] conditions = { currentPower == 0, currentPower == 1, currentPower == 2, currentPower == 3, currentPower == 4 };
            int correct3 = Array.IndexOf(conditions, true);
            List<List<Vector2>> places = new List<List<Vector2>> { textureBasicPosition, textureFirePosition, textureWaterPosition, textureAirPosition, textureEarthPosition };

            for (int j = 0; j < places.ToArray().Length; j++)
            {
                if (correct3 == j)
                {
                    texturePositionsInSprite = places[j];
                    break;
                }
            }
            for (int i = 0; i < upToWhichFrame; i++)
            {
                texturePositionsInSprite.Add(new Vector2(row, i));
            }
            return texturePositionsInSprite;
        }

        public void Update(MyGame game, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void PlayJumpSoundEffect()
        {
            soundEffectJump.CreateInstance().Play();
        }
    }
}
