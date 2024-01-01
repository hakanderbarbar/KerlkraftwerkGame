using KerlkraftwerkGame.Global;
using KerlkraftwerkGame.Managers;

namespace KerlkraftwerkGame.Entities
{
    public class Character
    {
        private static Texture2D texture;
        private readonly Animation anim;
        private Vector2 position;
        private Vector2 velocity;
        private float jumpSpeed = 650f;
        private float gravity = 1000f; // Dieser Wert soll sich gleichzeitig mit der Map ändern
        private int jumpsRemaining = 2;
        private float jumpDuration = 0.1f;
        private float jumpTimer = 0f;
        private float groundLevel = 430f;

        private bool isJumping = false;
        private bool isFalling = false;

        private Rectangle boundingBox; // Kollisions-Rechteck

        public Character(Vector2 startPosition)
        {
            texture ??= Globals.Content.Load<Texture2D>("Run");
            this.anim = new Animation(texture, 8, 0.1f);
            this.position = startPosition;

            // Initialisiere das boundingBox-Rechteck
            this.boundingBox = new Rectangle((int)this.position.X, (int)this.position.Y, this.anim.FrameWidth, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            this.anim.Update();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.isJumping)
            {
                this.UpdateJump(deltaTime);
            }
            else
            {
                this.UpdateFall(deltaTime);
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
                    this.velocity.Y = -this.jumpSpeed / 2f; // Second Jump (mit halber Geschwindigkeit)
                }
            }
        }

        public void SetGravity(float newGravity)
        {
            // Methode zum Setzen der Gravitation
            this.gravity = newGravity;
        }

        public void Draw()
        {
            this.anim.Draw(this.position);
        }

        public Rectangle GetBoundingBox()
        {
            return this.boundingBox;
        }

        private void UpdateFall(float deltaTime)
        {
            this.velocity.Y += this.gravity * deltaTime;
            this.position += this.velocity * deltaTime;

            if (this.position.Y + texture.Height > this.groundLevel)
            {
                this.position.Y = this.groundLevel - texture.Height;
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

        private void UpdateJump(float deltaTime)
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
    }
}