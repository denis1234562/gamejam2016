using GameJam2016.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameJam2016
{
    public class Player : IScene
    {
        public Vector2 PlayerLocation;
        public Rectangle PlayerSize;

        private bool right, left, jumping;
        private AnimatedSprite animatedSprite;
        private SoundEffect soundEffectJump;
        public float startX = 200;
        public float startY = 300; //550;

        public Player()
        {
            PlayerLocation = new Vector2(startX, startY);
        }

        public void Update(MyGame game, GameTime gameTime, PlayerAction action)
        {
            if ((action & PlayerAction.MoveRight) == PlayerAction.MoveRight)
            {
                int row = 7;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2),
                                                               new Vector2(row, 2),new Vector2(row, 3),new Vector2(row, 4),
                                                               new Vector2(row, 5),new Vector2(row, 6),new Vector2(row, 7) ,
                                                               new Vector2(row, 8), new Vector2(row , 9)};
                right = true;
                left = false;
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
            }
            else if (right)
            {

                right = false;
                int row = 3;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 2) };
            }
            else if (left)
            {
                left = false;
                int row = 1;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2) };
            }

            animatedSprite.Update(gameTime);

            PlayerSize.X = (int)PlayerLocation.X;
            PlayerSize.Y = (int)PlayerLocation.Y;
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            animatedSprite.Draw(game.spriteBatch, PlayerLocation);
        }

        public void UnloadContent(MyGame myGame)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(MyGame myGame)
        {
            soundEffectJump = myGame.Content.Load<SoundEffect>("Sounds/238282__meroleroman7__robot-jump-2");
            Texture2D texture = myGame.Content.Load<Texture2D>("linkEdit");
            animatedSprite = new AnimatedSprite(texture, 8, 10, new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2) });

            PlayerSize = new Rectangle(0, 0, texture.Width / animatedSprite.Columns, texture.Height / animatedSprite.Rows);
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
