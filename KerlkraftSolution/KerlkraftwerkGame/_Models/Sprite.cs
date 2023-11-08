namespace KerlkraftwerkGame;

public class Sprite
{
    public Texture2D Texture { get; }

    public Vector2 Position;

    public Sprite(Texture2D texture, Vector2 position)
    {
        this.Texture = texture;
        this.Position = position;
    }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(this.Texture, this.Position, Color.White);
    }
}
