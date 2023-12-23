using KerlkraftwerkGame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace KerlkraftwerkGame.System;

public class InputController
{
    private KeyboardState previousKeyboardState;

    public void Update(GameTime gameTime, Character character)
    {
        KeyboardState currentKeyboardState = Keyboard.GetState();

        // Der Charakter bewegt sich ständig nach vorne (Rennen)
        // Springen, wenn die Leertaste gedrückt wird
        if (currentKeyboardState.IsKeyDown(Keys.Space) && !this.previousKeyboardState.IsKeyDown(Keys.Space))
        {
            character.Jump();
        }

        this.previousKeyboardState = currentKeyboardState;
    }

    public bool ShouldExit()
    {
        return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape);
    }
}