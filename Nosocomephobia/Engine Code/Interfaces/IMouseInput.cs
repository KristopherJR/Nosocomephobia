using Microsoft.Xna.Framework.Input;

namespace Nosocomephobia.Engine_Code.Interfaces
{
    interface IMouseInput
    {
        /// <summary>
        /// gets the current state of the Mouse and returns it.
        /// </summary>
        /// <returns>The current state of the mouse.</returns>
        MouseState GetCurrentState();
    }
}
