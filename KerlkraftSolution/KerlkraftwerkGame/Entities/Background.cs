using System;
using KerlkraftwerkGame.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KerlkraftwerkGame.Entities
{
    public class Background
    {
        private Texture2D backgroundTexture;
        private string currentTextureName;

        public Background(string initialMap)
        {
            this.ChangeTexture(initialMap);
        }

        public string CurrentTextureName
        {
            get { return this.currentTextureName; }
        }

        public void ChangeTexture(string newMap)
        {
            this.currentTextureName = newMap;
            this.backgroundTexture = Globals.Content.Load<Texture2D>(newMap);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(this.backgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, this.CalculateBackgroundScale(), SpriteEffects.None, 0f);
        }

        private float CalculateBackgroundScale()
        {
            float scaleWidth = (float)Globals.GraphicsDevice.Viewport.Width / this.backgroundTexture.Width;
            float scaleHeight = (float)Globals.GraphicsDevice.Viewport.Height / this.backgroundTexture.Height;

            return Math.Max(scaleWidth, scaleHeight);
        }
    }
}