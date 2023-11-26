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
        position.X += speed * deltaTime;

        // Hier könntest du auch Kollisionsprüfungen oder andere Logik hinzufügen

        // Wenn der Charakter springt, die Y-Position anpassen
        if (isJumping)
        {
            position.Y += jumpSpeed * deltaTime;
            isJumping = false;  // Reset, um mehrere aufeinanderfolgende Sprünge zu verhindern
        }
    }

    public void Jump()
    {
        // Wenn der Charakter nicht bereits springt, springen ermöglichen
        if (!isJumping)
        {
            isJumping = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }
}