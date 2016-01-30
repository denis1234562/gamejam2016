using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace GameJam2016.Objects
{
    public class TileMap : IScene
    {
        public const float BOTTOM_OFFSET = 120f;
        public const float WIDTH = 150f;
        public const float HEIGHT = 100f;
        public const float SPEED = 100f;

        private float offset;
        private Texture2D tile1;
        private Texture2D tile2;
        private string tile1filename;
        private string tile2filename;
        private string backgroundClass;

        public List<List<Tile>> Tiles;

        public TileMap(string mapFilename)
        {
            using (var sr = new StreamReader(mapFilename))
            {
                backgroundClass = sr.ReadLine();
                tile1filename = sr.ReadLine();
                tile2filename = sr.ReadLine();

                var indexX = 0;
                Tiles = new List<List<Tile>>();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line.StartsWith("-")) continue;
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    Tiles.Add(new List<Tile>());

                    var ss = line.ToCharArray();

                    for (var i = 0; i < ss.Length; i++)
                    {
                        if (ss[i] == '.') continue;

                        var t = new Tile();
                        t.Type = ss[i];

                        Tiles[indexX].Add(t);
                    }

                    indexX++;
                }
            }
        }

        public void LoadContent(MyGame game)
        {
            tile1 = game.Content.Load<Texture2D>(tile1filename);
            tile2 = game.Content.Load<Texture2D>(tile2filename);
        }

        public void UnloadContent(MyGame game)
        {
            throw new NotImplementedException();
        }

        public void Update(MyGame game, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Update(MyGame game, GameTime gameTime, PlayerAction action)
        {
            //Get directional vector based on input
            float direction = 0f;

            if ((action & PlayerAction.MoveLeft) == PlayerAction.MoveLeft)
            {
                direction += -1f;
            }
            else if ((action & PlayerAction.MoveRight) == PlayerAction.MoveRight)
            {
                direction += 1f;
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float distance = direction * SPEED * elapsed;
            offset += distance;
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            var spriteBatch = game.spriteBatch;

            spriteBatch.Begin();
            for (var x = 0; x < this.Tiles.Count; x++)
            {
                var tileOffset = x * WIDTH - offset;

                if (tileOffset < -WIDTH) continue;
                if (tileOffset >= game.GraphicsDevice.Viewport.Width + WIDTH) continue;

                var location = new Vector2(offset - tileOffset, 0);
                for (var y = 0; y < this.Tiles[x].Count; y++)
                {
                    location.Y = game.GraphicsDevice.Viewport.Height - y * HEIGHT - BOTTOM_OFFSET;

                    var tile = Tiles[x][y];
                    var texture = getTextureByTileType(tile.Type);

                    spriteBatch.Draw(texture, location, Color.White);
                }
            }

            spriteBatch.End();
        }

        private Texture2D getTextureByTileType(char type)
        {
            if (type == ' ')
            {
                return tile2;
            }

            return tile1;
        }
    }
}
