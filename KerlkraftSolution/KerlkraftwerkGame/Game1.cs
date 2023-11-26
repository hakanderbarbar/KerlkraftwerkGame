using System;

public class Game1 : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D backgroundTexture;
    Character mainCharacter;
    InputController inputController;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // Lade den Hintergrund
        backgroundTexture = Content.Load<Texture2D>("background");

        // Lade den Charakter
        Texture2D characterTexture = Content.Load<Texture2D>("mainCharacter");
        mainCharacter = new Character(characterTexture, new Vector2(100, 300));

        // Initialisiere den InputController
        inputController = new InputController();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Aktualisiere den InputController und den Charakter
        inputController.Update(gameTime, mainCharacter);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();

        // Skaliere und positioniere den Hintergrund, um ihn an den Bildschirm anzupassen
        float scaleWidth = (float)GraphicsDevice.Viewport.Width / backgroundTexture.Width;
        float scaleHeight = (float)GraphicsDevice.Viewport.Height / backgroundTexture.Height;

        float scale = Math.Max(scaleWidth, scaleHeight); // Verwende den größten Skalierungsfaktor

        spriteBatch.Draw(backgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

        // Zeichne den Charakter
        mainCharacter.Draw(spriteBatch);

        spriteBatch.End();

        base.Draw(gameTime);
    }


}