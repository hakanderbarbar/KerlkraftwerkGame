﻿using Microsoft.Xna.Framework.Content;

namespace KerlkraftwerkGame.Global
{
    public static class Globals
    {
        public static float Time { get; set; }

        public static float TotalSeconds { get; set; }

        public static ContentManager Content { get; set; }

        public static SpriteBatch SpriteBatch { get; set; }

        public static GraphicsDevice GraphicsDevice { get; set; }

        public static Point WindowSize { get; set; }

        public static void Update(GameTime gt)
        {
            Time = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}