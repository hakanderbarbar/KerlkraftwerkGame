using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerlkraftwerkGame._Managers
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
            aTexture = texture;
            aFrameTime = frameTime;
            aFrameTimeLeft = aFrameTime;
            aFrames = framesX;
            var frameWidth = aTexture.Width / framesX;
            var frameHeight = aTexture.Height;

            for (int i = 0; i < aFrames; i++)
            {
                aSourceRectangles.Add(new (i * frameWidth, 0, frameWidth, frameHeight));
            }
        }

        public void Start()
        {
            aActive = true;
        }

        public void Stop()
        {
            aActive = false;
        }

        public void Reset()
        {
            aFrame = 0;
            aFrameTimeLeft = aFrameTime;
        }

        public void Update()
        {
            if (!aActive)
            {
                return;
            }

            aFrameTimeLeft -= Globals.TotalSeconds;

            if (aFrameTime <= 0)
            {
                aFrameTimeLeft += aFrameTime;
                aFrame = (aFrame + 1) % aFrames;
            }
        }

        public void Draw(Vector2 pos)
        {
            Globals.SpriteBatch.Draw(aTexture, pos, aSourceRectangles[aFrame], Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }
    }
}
