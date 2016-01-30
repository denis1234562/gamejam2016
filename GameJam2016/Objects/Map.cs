using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2016.Objects
{
    public class TileMap : IScene
    {
        private Texture2D tileset;

        public TileMap(string mapFilename)
        {

        }

        public void LoadContent(MyGame game)
        {
            tileset = game.Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
        }

        public void UnloadContent(MyGame game)
        {
            throw new NotImplementedException();
        }

        public void Update(MyGame game, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(MyGame game, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
