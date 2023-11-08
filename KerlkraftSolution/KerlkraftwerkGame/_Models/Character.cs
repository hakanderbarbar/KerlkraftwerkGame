using KerlkraftwerkGame;

namespace KerlkraftwerkGame;

public class Character : Sprite
{
    private const float SPEED = 750f;
    private const float GRAVITY = 5000f;
    private const float JUMP = 1500f;
    private const int OFFSET = 10;
    private Vector2 velocity;
    private bool onGround;

    public Character(Texture2D texture, Vector2 position)
        : base(texture, position)
    {
    }

    private Rectangle CalculateBounds(Vector2 pos)
    {
        return new ((int)pos.X + OFFSET, (int)pos.Y, this.Texture.Width - (2 * OFFSET), this.Texture.Height);
    }

    private void UpdateVelocity()
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.A)) this.velocity.X = -SPEED;
        else if (keyboardState.IsKeyDown(Keys.D)) this.velocity.X = SPEED;
        else this.velocity.X = 0;

        this.velocity.Y += GRAVITY * Globals.Time;

        if (keyboardState.IsKeyDown(Keys.Space) && this.onGround)
        {
            this.velocity.Y = -JUMP;
        }
    }

    private void UpdatePosition()
    {
        this.onGround = false;
        var newPos = this.position + (this.velocity * Globals.Time);
        Rectangle newRect = this.CalculateBounds(newPos);

        foreach (var collider in Map.GetNearestColliders(newRect))
        {
            if (newPos.X != this.position.X)
            {
                newRect = this.CalculateBounds(new(newPos.X, this.position.Y));
                if (newRect.Intersects(collider))
                {
                    if (newPos.X > this.position.X) newPos.X = collider.Left - this.Texture.Width + OFFSET;
                    else newPos.X = collider.Right - OFFSET;
                    continue;
                }
            }

            newRect = this.CalculateBounds(new(this.position.X, newPos.Y));
            if (newRect.Intersects(collider))
            {
                if (this.velocity.Y > 0)
                {
                    newPos.Y = collider.Top - this.Texture.Height;
                    this.onGround = true;
                    this.velocity.Y = 0;
                }
                else
                {
                    newPos.Y = collider.Bottom;
                    this.velocity.Y = 0;
                }
            }
        }

        this.position = newPos;
    }

    public void Update()
    {
        this.UpdateVelocity();
        this.UpdatePosition();
    }
}
