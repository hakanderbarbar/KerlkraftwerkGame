using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KerlkraftwerkGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Character character;
        private int screenWidth = 1280;
        private int screenHeight = 720;
        private float scale = 0.5f;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            // Erstelle die Figur
            this.character = new Character(this.GraphicsDevice);
            this.character.LoadContent(this.Content);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // Je nachdem welche Taste gedrückt, wird hier eine entsprechende Reaktion gezeigt
            // if (state.IsKeyDown(Keys.Up))
            // {
             //   moveDirection.Y -= 1;
            // }

            // Aktualisiere die Character-Klasse
            this.character.Update();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();

            // Lade und zeichne den Hintergrund
            Texture2D backgroundTexture = this.Content.Load<Texture2D>("background");

            // Hintergrund auf Bildschirmgröße angepasst
            Rectangle destRect = new Rectangle(0, 0, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height);
            this.spriteBatch.Draw(backgroundTexture, destRect, Color.White);

            // Figur zeichnen
            this.character.Draw(this.spriteBatch);

            // Zeichnen beenden
            this.spriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}