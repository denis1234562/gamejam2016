using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameJam2016
{
    public interface IScene
    {
        void Update(MyGame game, GameTime gameTime);
        void Draw(MyGame game, GameTime gameTime);
        void UnloadContent(MyGame myGame);
        void LoadContent(MyGame myGame);
    }
}
