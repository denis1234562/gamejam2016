using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJam2016
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch Anim;
        private AnimatedSprite animatedSprite;
        private string _stringValue = string.Empty;
        bool right = false;
        bool left = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Anim = new SpriteBatch(GraphicsDevice);
            Texture2D texture = Content.Load<Texture2D>("linkEdit");
            animatedSprite = new AnimatedSprite(texture, 8, 10, new Vector2[] {new Vector2(0,0), new Vector2(0, 1), new Vector2(0, 2) });
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            animatedSprite.Update(gameTime);
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightStick))
            {
                int row = 7;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2),
                                                               new Vector2(row, 2),new Vector2(row, 3),new Vector2(row, 4),
                                                               new Vector2(row, 5),new Vector2(row, 6),new Vector2(row, 7) ,
                                                               new Vector2(row, 8), new Vector2(row , 9)};
                right = true;
                left = false;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftStick))
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
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2) } ;
            }
            else if (left)
            {
                left = false;
                int row = 1;
                animatedSprite.Animation = new Vector2[] { new Vector2(row, 0), new Vector2(row, 1), new Vector2(row, 2) };
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            animatedSprite.Draw(Anim, new Vector2(400, 200));
            base.Draw(gameTime);
        }
    }
}
