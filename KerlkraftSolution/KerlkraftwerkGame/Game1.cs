using System;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private Texture2D backgroundTexture;
    private Character mainCharacter;
    private InputController inputController;

    public Game1()
    {
        this.graphics = new GraphicsDeviceManager(this);
        this.Content.RootDirectory = "Content";
        this.IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

        // Lade den Hintergrund
        this.backgroundTexture = this.Content.Load<Texture2D>("background");

        // Lade den Charakter
        Texture2D characterTexture = this.Content.Load<Texture2D>("mainCharacter");
        this.mainCharacter = new Character(characterTexture, new Vector2(100, 300));

        // Initialisiere den InputController
        this.inputController = new InputController();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            this.Exit();
        }

        // Aktualisiere den InputController und den Charakter
        this.inputController.Update(gameTime, this.mainCharacter);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.CornflowerBlue);

        this.spriteBatch.Begin();

        // Skaliere und positioniere den Hintergrund, um ihn an den Bildschirm anzupassen
        float scaleWidth = (float)this.GraphicsDevice.Viewport.Width / this.backgroundTexture.Width;
        float scaleHeight = (float)this.GraphicsDevice.Viewport.Height / this.backgroundTexture.Height;

        float scale = Math.Max(scaleWidth, scaleHeight); // Verwende den größten Skalierungsfaktor

        this.spriteBatch.Draw(this.backgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

        // Zeichne den Charakter
        this.mainCharacter.Draw(this.spriteBatch);

        this.spriteBatch.End();

        base.Draw(gameTime);
    }
}