using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace KerlkraftwerkGame.Content;

    public static class Globals
    {
        public static float Time { get; set; }




        public static void Update(GameTime gameTime)
    {
        Time = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    }

