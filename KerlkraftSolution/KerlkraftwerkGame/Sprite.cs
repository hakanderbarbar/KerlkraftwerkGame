using KerlkraftwerkGame.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerlkraftwerkGame
{



    public class Sprite
    {
        public Texture2D Texture { get; }
        public Vector2 position;

        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            this.position = position;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, position, Color.White);
        }
    }
}