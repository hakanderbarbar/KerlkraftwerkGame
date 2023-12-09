public class Character
{
    private Texture2D texture;
    private Vector2 position;
    private float jumpSpeed = 300f;
    private float gravity = 500f;
    private bool isJumping = false;
    private float jumpDuration = 0.5f;
    private float jumpTimer = 0f;
    private float groundLevel = 430f; // Startposition Character

    public Character(Texture2D texture, Vector2 startPosition)
    {
        this.texture = texture;
        this.position = startPosition;
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (this.isJumping)
        {
            this.position.Y -= this.jumpSpeed * deltaTime;

            // Aktualisiere den Sprung-Timer
            this.jumpTimer += deltaTime;

            if (this.jumpTimer >= this.jumpDuration)
            {
                this.isJumping = false;
                this.jumpTimer = 0f;
            }
        }
        else
        {
            // Der Charakter wird durch die Gravitation nach unten gezogen
            this.position.Y += this.gravity * deltaTime;

            // Kollisionsabfrage mit der festgelegten Y-Position (groundLevel)
            if (this.position.Y + this.texture.Height > this.groundLevel)
            {
                this.position.Y = this.groundLevel - this.texture.Height;
            }
        }
    }

    public void Jump()
    {
        // Wenn der Charakter nicht bereits springt, starte den Sprung
        if (!this.isJumping)
        {
            this.isJumping = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.texture, this.position, Color.White);
    }
}
