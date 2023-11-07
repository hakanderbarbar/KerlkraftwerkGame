using KerlkraftwerkGame.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KerlkraftwerkGame
{



    public class Character
    {
        private GraphicsDevice graphicsDevice;
        private float groundY = 350; // 350 perfekt auf dem Boden
        private Texture2D texture;
        private Vector2 position;

        private Vector2 velocity;
        private float JUMP = 1500f;
        private float SPEED = 750f;
        private float GRAVITY = 5000f;
        private bool isOnGround = true;

        public Character(GraphicsDevice graphicsDevice) //Texture2D characterTexture, Vector2 initialPosition,
        {
            //this.texture = characterTexture;
            //this.position = initialPosition;
            this.graphicsDevice = graphicsDevice;
        }

        public void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("mainCharacter");

            this.position.X = 30;
            this.position.Y = this.graphicsDevice.Viewport.Height - this.texture.Height;
        }

        private void UpdateVelocity()
        {
            var keyboardState = Keyboard.GetState();

            if (!isOnGround) velocity.Y += GRAVITY * Globals.Time;

            if (keyboardState.IsKeyDown(Keys.Up) && isOnGround)
            {
                this.velocity.Y = -JUMP;
                this.isOnGround = false;
            }
        }


        public void Update()
        {
            UpdateVelocity();
            position += velocity * Globals.Time;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, Color.White);
        }
    }
}