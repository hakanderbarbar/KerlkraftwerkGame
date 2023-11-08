using System;
using System.Collections.Generic;

namespace KerlkraftwerkGame;

public class Map
{
    public static readonly int tilesize = 128;
    public static readonly int[,] Tiles = {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 2, 2, 0, 1},
        {1, 0, 0, 2, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 2, 2, 2, 0, 1},
        {1, 0, 0, 2, 2, 1, 1, 1, 2, 1},
        {1, 2, 2, 1, 1, 1, 1, 1, 1, 1},
    };

    private readonly RenderTarget2D target;

    private static Rectangle[,] Colliders { get; } = new Rectangle[Tiles.GetLength(0), Tiles.GetLength(1)];

    public Map()
    {
        this.target = new (Globals.GraphicsDevice, Tiles.GetLength(1) * tilesize, Tiles.GetLength(0) * tilesize);

        //Auf tile1 und 2 kann nicht zugegriffen werden, da es keine .xnb Dateien gibt im Content/bin/DesktopGL/Content Ordner

        var tile1tex = Globals.Content.Load<Texture2D>("obstacle"); //Hier muss tile1 statt obstacle stehen
        var tile2tex = Globals.Content.Load<Texture2D>("obstacle"); //Hier muss tile2 statt obstacle stehen

        Globals.GraphicsDevice.SetRenderTarget(this.target);
        Globals.GraphicsDevice.Clear(Color.Transparent);
        Globals.SpriteBatch.Begin();

        for (int x = 0; x < Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                if (Tiles[x, y] == 0) continue;
                var posX = y * tilesize;
                var posY = x * tilesize;
                var tex = Tiles[x, y] == 1 ? tile1tex : tile2tex;
                Colliders[x, y] = new(posX, posY, tilesize, tilesize);

                Globals.SpriteBatch.Draw(tex, new Vector2(posX, posY), Color.White);
            }
        }

        Globals.SpriteBatch.End();
        Globals.GraphicsDevice.SetRenderTarget(null);
    }

    public static List<Rectangle> GetNearestColliders(Rectangle bounds)
    {
        int leftTile = (int)Math.Floor((float)bounds.Left / tilesize);
        int rightTile = (int)Math.Ceiling((float)bounds.Right / tilesize) - 1;
        int topTile = (int)Math.Floor((float)bounds.Top / tilesize);
        int bottomTile = (int)Math.Ceiling((float)bounds.Bottom / tilesize) - 1;

        leftTile = MathHelper.Clamp(leftTile, 0, Tiles.GetLength(1));
        rightTile = MathHelper.Clamp(rightTile, 0, Tiles.GetLength(1));
        topTile = MathHelper.Clamp(topTile, 0, Tiles.GetLength(0));
        bottomTile = MathHelper.Clamp(bottomTile, 0, Tiles.GetLength(0));

        List<Rectangle> result = new ();

        for (int x = topTile; x <= bottomTile; x++)
        {
            for (int y = leftTile; y <= rightTile; y++)
            {
                if (Tiles[x, y] != 0) result.Add(Colliders[x, y]);
            }
        }

        return result;
    }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(this.target, Vector2.Zero, Color.White);
    }
}