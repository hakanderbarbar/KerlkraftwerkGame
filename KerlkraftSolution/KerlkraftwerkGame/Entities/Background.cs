using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KerlkraftwerkGame.Global;


namespace KerlkraftwerkGame.Entities
{

    public class Background
    {
        private Texture2D backgroundTexture;

        public Background()
        {
            this.backgroundTexture = Globals.Content.Load<Texture2D>("background");
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
