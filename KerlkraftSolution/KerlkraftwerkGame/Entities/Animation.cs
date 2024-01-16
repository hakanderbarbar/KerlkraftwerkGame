using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KerlkraftwerkGame.Global;

namespace KerlkraftwerkGame.Entities
{
    public class Animation
    {
        private readonly Texture2D aTexture;
        private readonly List<Rectangle> aSourceRectangles = new();
        private readonly int aFrames;
        private readonly float aFrameTime;
        private int aFrame;
        private float aFrameTimeLeft;

        public Animation(Texture2D texture, int framesX, float frameTime)
        {
            this.aTexture = texture;
            this.aFrameTime = frameTime;
            this.aFrameTimeLeft = this.aFrameTime;
            this.aFrames = framesX;
            this.FrameWidth = this.aTexture.Width / framesX;
            var frameHeight = this.aTexture.Height;

            for (int i = 0; i < this.aFrames; i++)
            {
                this.aSourceRectangles.Add(new (i * this.FrameWidth, 0, this.FrameWidth, frameHeight));
            }
        }

        public int FrameWidth { get; private set; }

        public void Update()
        {
            this.aFrameTimeLeft -= Globals.TotalSeconds;

            if (this.aFrameTimeLeft <= 0)
            {
                this.aFrameTimeLeft += this.aFrameTime;
                this.aFrame = (this.aFrame + 1) % this.aFrames;
            }
        }

        public void Draw(Vector2 pos)
        {
            Globals.SpriteBatch.Draw(this.aTexture, pos, this.aSourceRectangles[this.aFrame], Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }
    }
}