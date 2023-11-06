using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Character
{
    private GraphicsDevice graphicsDevice;
    private float groundY = 350; // 350 perfekt auf dem Boden
    private Texture2D texture;
    private Vector2 position;
    private Vector2 velocity;
    private float jumpStrength = -10f;
    private float moveSpeed = 1.0f;
    private float gravity = 0.5f;

    private bool isOnGround = true;

    public Character(Texture2D characterTexture, Vector2 initialPosition)
    {
        this.texture = characterTexture;
        this.position = initialPosition;
    }

    // Konstruktor damit die Figur erstellt werden kann
    public Character(GraphicsDevice graphicsDevice)
    {
        this.graphicsDevice = graphicsDevice;
    }

    public void LoadContent(ContentManager content)
    {
        this.texture = content.Load<Texture2D>("mainCharacter");

        this.position.X = 30;
        this.position.Y = this.graphicsDevice.Viewport.Height - this.texture.Height;
    }

    public void Jump()
    {
        if (this.isOnGround)
        {
            this.velocity.Y = this.jumpStrength;
            this.isOnGround = false;
        }
    }

    public void Update()
    {
        if (!this.isOnGround)
        {
            this.velocity.Y += this.gravity;
        }

        if (this.position.Y >= this.groundY)
        {
            this.position.Y = this.groundY;
            this.isOnGround = true;
            this.velocity.Y = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.texture, this.position, Color.White);
    }
}