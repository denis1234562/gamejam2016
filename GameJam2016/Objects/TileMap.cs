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
        private Texture2D tileset;
        public List<List<Tile>> Tiles;

        public TileMap(string mapFilename)
        {
            ////using (var sr = new StreamReader(mapFilename))
            ////{
            ////    var backgroundClass = sr.ReadLine();
            ////    var tilesFilename = sr.ReadLine();
            ////    var indexX = 0;
            ////    Tiles = new List<List<Tile>>();

            ////    while (!sr.EndOfStream)
            ////    {
            ////        var line = sr.ReadLine();
            ////        if (line.StartsWith("-")) continue;
            ////        if (string.IsNullOrWhiteSpace(line)) continue;

            ////        Tiles[indexX] = new List<Tile>();

            ////        var ss = line.ToCharArray();

            ////        for (var i = 0; i < ss.Length; i++)
            ////        {
            ////            if (ss[i] == '.') continue;

            ////            var t = new Tile();
            ////            t.Type = ss[i];

            ////            Tiles[indexX].Add(t);                        
            ////        }
            ////    }
            ////}
        }

        public void LoadContent(MyGame game)
        {
            //tileset = game.Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
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
