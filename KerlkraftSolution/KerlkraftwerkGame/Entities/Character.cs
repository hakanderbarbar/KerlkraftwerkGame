public class Character
{
    private Texture2D texture;
    private Vector2 position;
    private Vector2 velocity;
    private float jumpSpeed = 650f;
    private float gravity = 1000f;
    private int jumpsRemaining = 2;
    private float jumpDuration = 0.1f;
    private float jumpTimer = 0f;
    private float groundLevel = 430f;

    private bool isJumping = false;
    private bool isFalling = false;

    private Rectangle boundingBox; // Kollisions-Rechteck

    public Character(Texture2D texture, Vector2 startPosition)
    {
        this.texture = texture;
        this.position = startPosition;
        this.velocity = Vector2.Zero;

        // Initialisiere das boundingBox-Rechteck
        this.boundingBox = new Rectangle((int)this.position.X, (int)this.position.Y, texture.Width, texture.Height);
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (this.isJumping)
        {
            this.position.Y -= this.jumpSpeed * deltaTime;
            this.jumpTimer += deltaTime;

            if (this.jumpTimer >= this.jumpDuration)
            {
                this.isJumping = false;
                this.jumpTimer = 0f;
                this.isFalling = true;
            }
        }
        else
        {
            this.velocity.Y += this.gravity * deltaTime;
            this.position += this.velocity * deltaTime;

            if (this.position.Y + this.texture.Height > this.groundLevel)
            {
                this.position.Y = this.groundLevel - this.texture.Height;
                this.velocity.Y = 0f;

                if (this.isFalling)
                {
                    this.isFalling = false;
                    this.jumpsRemaining = 2;
                }
            }
            else
            {
                this.isFalling = true;
            }
        }

        // Aktualisiere das boundingBox-Rechteck
        this.boundingBox.X = (int)this.position.X;
        this.boundingBox.Y = (int)this.position.Y;
    }

    public void Jump()
    {
        if (this.jumpsRemaining > 0 && !this.isJumping)
        {
            this.isJumping = true;
            this.jumpsRemaining--;

            // Setze die Sprunggeschwindigkeit basierend auf der Anzahl der verbleibenden Sprünge
            if (this.jumpsRemaining == 1)
            {
                this.velocity.Y = -this.jumpSpeed; // First Jump
            }
            else if (this.jumpsRemaining == 0)
            {
                this.velocity.Y = -this.jumpSpeed / 2f; // Second Jump (mit halber Geschwíndigkeit)
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.texture, this.position, Color.White);
    }

    public Rectangle GetBoundingBox()
    {
        return this.boundingBox;
    }
}