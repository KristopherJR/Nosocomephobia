using Microsoft.Xna.Framework.Input;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
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
