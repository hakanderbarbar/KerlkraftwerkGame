using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
//using TrexRunner.Entities;

namespace KerlkraftwerkGame.System
{
    public class InputController
    {

        private bool isBlocked;
        private character character;

        private KeyboardState previousKeyboardState;

        public InputController(Trex character)
        {
            character = character;
        }

        public void ProcessControls(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();

            if (!isBlocked)
            {
                bool isJumpKeyPressed = keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Space);
                bool wasJumpKeyPressed = previousKeyboardState.IsKeyDown(Keys.Up) || previousKeyboardState.IsKeyDown(Keys.Space);

                if (!wasJumpKeyPressed && isJumpKeyPressed)
                {

                    if (character.State != TrexState.Jumping)
                        character.BeginJump();

                }
                else if (character.State == TrexState.Jumping && !isJumpKeyPressed)
                {

                    character.CancelJump();

                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {

                    if (character.State == TrexState.Jumping || character.State == TrexState.Falling)
                        character.Drop();
                    else
                        character.Duck();

                }
                else if (character.State == TrexState.Ducking && !keyboardState.IsKeyDown(Keys.Down))
                {

                    character.GetUp();

                }

            }

            previousKeyboardState = keyboardState;

            isBlocked = false;

        }

        public void BlockInputTemporarily()
        {
            isBlocked = true;
        }

    }
}
