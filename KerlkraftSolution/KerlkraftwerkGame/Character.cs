using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Character
{
    private GraphicsDevice graphicsDevice;
    private float groundY = 350; //350 perfekt auf dem Boden
    private Texture2D texture;
    private Vector2 position;
    private Vector2 velocity;
    private float jumpStrength = -10f;
    private float moveSpeed = 3.0f;
    private float gravity = 0.5f;


    private bool isOnGround = true;

    public Character(Texture2D characterTexture, Vector2 initialPosition)
    {
        texture = characterTexture;
        position = initialPosition;
    }

    //Konstruktor damit die Figur erstellt werden kann
    public Character(GraphicsDevice graphicsDevice)
    {
        this.graphicsDevice = graphicsDevice;
    }



    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>("mainCharacter");


        position.X = 30;
        position.Y = graphicsDevice.Viewport.Height - texture.Height ; 
    }



        public void Jump()
    {
        if (isOnGround)
        {
            velocity.Y = jumpStrength;
            isOnGround = false;
        }
    }

    public void Update()
    {
        position.X += moveSpeed;

        if (!isOnGround)
        {
            velocity.Y += gravity;
        }

        if (position.Y >= groundY)
        {
            position.Y = groundY;
            isOnGround = true;
            velocity.Y = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }
}
