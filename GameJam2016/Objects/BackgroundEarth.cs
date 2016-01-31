using GameJam2016.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameJam2016.Objects
{
    public class BackgroundEarth : ParallaxBackground
    {
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
    }
}
