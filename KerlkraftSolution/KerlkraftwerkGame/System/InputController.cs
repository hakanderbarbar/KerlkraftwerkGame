using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputController
{
    private KeyboardState previousKeyboardState;

    public void Update(GameTime gameTime, Character character)
    {
        KeyboardState currentKeyboardState = Keyboard.GetState();

        // Der Charakter bewegt sich ständig nach vorne (Rennen)
        character.Update(gameTime);

        // Springen, wenn die Leertaste gedrückt wird
        if (currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
        {
            character.Jump();
        }

        previousKeyboardState = currentKeyboardState;
    }
}