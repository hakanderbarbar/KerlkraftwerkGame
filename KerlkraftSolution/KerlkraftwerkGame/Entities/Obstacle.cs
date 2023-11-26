using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KerlkraftwerkGame.Entities
{
    public class Obstacle
    {
        private Texture2D texture;
        private Vector2 position;
        private float speed = 300f;

        public Obstacle(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            this.position = startPosition;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.position.X -= this.speed * deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, Color.White);
        }
    }
}