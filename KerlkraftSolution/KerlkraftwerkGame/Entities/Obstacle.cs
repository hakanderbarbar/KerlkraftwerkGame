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

        public Obstacle(int viewportWidth)
        {
            this.position = new Vector2(viewportWidth, 300);
        }

        // Öffentliche Eigenschaft (Property) für den Zugriff auf die Position
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public float ObstacleSpeed
        {
            get { return this.speed; }
        }

        // Öffentliche Eigenschaft (Property) für den Zugriff auf die Breite des Hindernisses
        public int Width
        {
            get { return this.texture.Width; }
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

        // Füge diese Methode hinzu, um das Kollisionsrechteck des Hindernisses abzurufen
        public Rectangle GetBoundingBox()
        {
            return new Rectangle((int)this.position.X, (int)this.position.Y, this.texture.Width, this.texture.Height);
        }

        public void SetSpeed(float newSpeed)
        {
            this.speed = newSpeed;
        }
    }
}