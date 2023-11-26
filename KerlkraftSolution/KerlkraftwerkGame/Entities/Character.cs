public class Character
{
    private Texture2D texture;
    private Vector2 position;
    private float speed = 5f;
    private float jumpSpeed = -10f;  // Geschwindigkeit des Sprungs
    private bool isJumping = false;

    public Character(Texture2D texture, Vector2 startPosition)
    {
        this.texture = texture;
        this.position = startPosition;
    }

    public void Update(GameTime gameTime)
    {
        // Der Charakter bewegt sich ständig nach vorne
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.position.X += this.speed * deltaTime;

        // Hier könntest du auch Kollisionsprüfungen oder andere Logik hinzufügen

        // Wenn der Charakter springt, die Y-Position anpassen
        if (this.isJumping)
        {
            this.position.Y += this.jumpSpeed * deltaTime;
            this.isJumping = false;  // Reset, um mehrere aufeinanderfolgende Sprünge zu verhindern
        }
    }

    public void Jump()
    {
        // Wenn der Charakter nicht bereits springt, springen ermöglichen
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