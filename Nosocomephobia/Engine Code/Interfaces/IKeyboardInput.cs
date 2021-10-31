using Microsoft.Xna.Framework.Input;

namespace Nosocomephobia.Engine_Code.Interfaces
{
    interface IKeyboardInput
    {
        /// <summary>
        /// gets the current state of the Keyboard and returns it.
        /// </summary>
        /// <returns>The current state of the keyboard.</returns>
        KeyboardState GetCurrentState();
    }
}
