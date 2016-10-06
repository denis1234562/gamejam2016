using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJam2016.Objects
{
    public class BackgroundAnimated : Background
    {
        private Texture2D[] textures;      // The images to use
        private float textureIndex;
        private float animationSpeed;
       
        public BackgroundAnimated(Texture2D[] textures, Vector2 speed, float zoom, float animationSpeed) : base(null, speed, zoom)
        {
            this.textures = textures;
            this.animationSpeed = animationSpeed;
        }

        public override void Update(GameTime gametime, Vector2 direction, Viewport viewport)
        {
            float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;

            // Store the viewport
            Viewport = viewport;

            // Calculate the distance to move our image, based on speed
            Vector2 distance = direction * Speed * elapsed;

            // Update our offset
            Offset += distance;
            textureIndex += elapsed * animationSpeed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var textureIndex2 = ((int)textureIndex) % textures.Length;
            spriteBatch.Draw(textures[textureIndex2], new Vector2(Viewport.X, Viewport.Y), Rectangle, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 1);
        }
    }
}
