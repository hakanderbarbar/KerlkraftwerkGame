using KerlkraftwerkGame.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KerlkraftwerkGame
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private GameManager gameManager;


        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
            graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
            this.graphics.ApplyChanges();

            Globals.Content = Content;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
            Globals.GraphicsDevice = GraphicsDevice;

            gameManager = new();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            Globals.Update(gameTime);
            gameManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Lade und zeichne den Hintergrund
            Texture2D backgroundTexture = this.Content.Load<Texture2D>("background");

            // Hintergrund auf Bildschirmgröße angepasst
            Rectangle destRect = new Rectangle(0, 0, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height);
            Globals.SpriteBatch.Draw(backgroundTexture, destRect, Color.White);

            gameManager.Draw();

            base.Draw(gameTime);
        }
    }
}