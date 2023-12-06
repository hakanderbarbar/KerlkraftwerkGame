using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KerlkraftwerkGame.Global;

namespace KerlkraftwerkGame.Managers
{
    public class Animation
    {
        private readonly Texture2D aTexture;
        private readonly List<Rectangle> aSourceRectangles = new ();
        private readonly int aFrames;
        private readonly float aFrameTime;
        private int aFrame;
        private float aFrameTimeLeft;
        private bool aActive = true;

        public Animation(Texture2D texture, int framesX, float frameTime)
        {
            this.aTexture = texture;
            this.aFrameTime = frameTime;
            this.aFrameTimeLeft = this.aFrameTime;
            this.aFrames = framesX;
            var frameWidth = this.aTexture.Width / framesX;
            var frameHeight = this.aTexture.Height;

            for (int i = 0; i < this.aFrames; i++)
            {
                this.aSourceRectangles.Add(new (i * frameWidth, 0, frameWidth, frameHeight));
            }
        }

        public void Start()
        {
            this.aActive = true;
        }

        public void Stop()
        {
            this.aActive = false;
        }

        public void Reset()
        {
            this.aFrame = 0;
            this.aFrameTimeLeft = this.aFrameTime;
        }

        public void Update()
        {
            if (!this.aActive)
            {
                return;
            }

            this.aFrameTimeLeft -= Globals.TotalSeconds;

            if (this.aFrameTime <= 0)
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