using KerlkraftwerkGame.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerlkraftwerkGame;
public class GameManager
{
    
    private readonly Character character;

    public GameManager()
    {
        
        this.character = new(Globals.Content.Load<Texture2D>("hero"), new(Globals.WindowSize.X / 2, 200));
    }

    public void Update()
    {
        character.Update();
    }

    public void Draw()
    {
        Globals.SpriteBatch.Begin();
        character.Draw();
        Globals.SpriteBatch.End();
    }
