﻿namespace KerlkraftwerkGame;

public class GameManager
{
    private readonly Map map;
    private readonly Character character;

    public GameManager()
    {
        this.map = new ();
        this.character = new (Globals.Content.Load<Texture2D>("Run"), new (Globals.WindowSize.X / 2, 200));
    }

    public void Update()
    {
        character.Update();
    }

    public void Draw()
    {
        Globals.SpriteBatch.Begin();
        this.map.Draw();
        this.character.Draw();
        Globals.SpriteBatch.End();
    }
}