namespace KerlkraftwerkGame;

public class Sprite
{
    private Vector2 position;

    public Sprite(Texture2D texture, Vector2 position)
    {
        this.Texture = texture;
        this.Position = position;
    }

    public Texture2D Texture { get; }

    public Vector2 Position { get => this.position; set => this.position = value; }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(this.Texture, this.Position, Color.White);
    }
}
