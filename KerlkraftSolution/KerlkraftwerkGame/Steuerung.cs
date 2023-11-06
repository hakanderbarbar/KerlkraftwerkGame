using Microsoft.Xna.Framework.Input;
using System;

namespace KerlkraftwerkGame
{
    public class Steuerung
    {
        private Character character;

        public Steuerung(Character character)
        {
            this.character = character;
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                character.Jump();
            }
        }
    }
}