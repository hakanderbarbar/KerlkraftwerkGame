using System;
using Microsoft.Xna.Framework.Input;

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
                this.character.Jump();
            }
        }
    }
}