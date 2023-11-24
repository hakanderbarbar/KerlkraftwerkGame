using KerlkraftwerkGame.Global;

namespace KerlkraftwerkGame;

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
        Globals.WindowSize = new (Map.Tiles.GetLength(1) * Map.Tilesize, Map.Tiles.GetLength(0) * Map.Tilesize);
        this.graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
        this.graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
        this.graphics.ApplyChanges();

        Globals.Content = this.Content;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Globals.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
        Globals.GraphicsDevice = this.GraphicsDevice;

        this.gameManager = new ();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            this.Exit();
        }

        Globals.Update(gameTime);
        this.gameManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.SkyBlue);

        this.gameManager.Draw();

        base.Draw(gameTime);
    }
}
