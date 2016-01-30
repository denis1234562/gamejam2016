using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameJam2016.Objects
{
    public class BackgroundAir : ParallaxBackground
    {
        public override void LoadContent(MyGame game)
        {
            backgrounds = new List<Background>();

            var fireTextures = new Texture2D[] {
                game.Content.Load<Texture2D>(@"Background\Air\1"),
                game.Content.Load<Texture2D>(@"Background\Air\2"),
                game.Content.Load<Texture2D>(@"Background\Air\3"),
                game.Content.Load<Texture2D>(@"Background\Air\4"),
                game.Content.Load<Texture2D>(@"Background\Air\5"),
                game.Content.Load<Texture2D>(@"Background\Air\6"),
                game.Content.Load<Texture2D>(@"Background\Air\7"),
                game.Content.Load<Texture2D>(@"Background\Air\8"),
                game.Content.Load<Texture2D>(@"Background\Air\9"),
                game.Content.Load<Texture2D>(@"Background\Air\10"),
                game.Content.Load<Texture2D>(@"Background\Air\11"),
                game.Content.Load<Texture2D>(@"Background\Air\12"),
                game.Content.Load<Texture2D>(@"Background\Air\13"),
                game.Content.Load<Texture2D>(@"Background\Air\14"),
                game.Content.Load<Texture2D>(@"Background\Air\15"),
                game.Content.Load<Texture2D>(@"Background\Air\16"),
            };
            backgrounds.Add(new BackgroundAnimated(fireTextures, new Vector2(5, 0), 3f, 6f));

            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_03_1920 x 1080"), new Vector2(100, 0), 0.9f));
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_02_1920 x 1080"), new Vector2(300, 0), 1f));
            backgrounds.Add(new Background(game.Content.Load<Texture2D>(@"Background\Dark\layer_01_1920 x 1080"), new Vector2(300, 0), 0.7f));

            // hack to position the background propery
            backgrounds[backgrounds.Count - 2].Offset.Y = 120;
            backgrounds[backgrounds.Count - 1].Offset.Y = 100;
        }
    }
}
