using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2016.Objects.PlayerSpells
{
    public class FireBall : IScene
    {
        public float Width;
        public float Height;
        public float Speed;
        public Vector2 FireBallPosition;
        public Texture2D texture;
        private AnimatedSprite animatedSprite;

        public FireBall()
        {
            FireBallPosition = new Vector2(100, 100);
        }
        public void Update(MyGame game, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        public void LoadContent(MyGame myGame)
        {
            texture = myGame.Content.Load<Texture2D>("HeroSpells/FireBall/fireball");
            animatedSprite = new AnimatedSprite(texture, 3, 3, new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2) });
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(texture, FireBallPosition, null, Color.White, 0, Vector2.Zero, .7f, SpriteEffects.None, 0);//fire
            game.spriteBatch.Begin();
        }

        public void UnloadContent(MyGame myGame)
        {
            throw new NotImplementedException();
        }

    }
}
