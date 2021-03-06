using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Interfaces;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
namespace Nosocomephobia.Engine_Code.InputClasses
{
    class MouseInput : IMouseInput
    {
        /// <summary>
        /// Gets the current state of the Mouse and returns it.
        /// </summary>
        /// <returns>The current state of the mouse.</returns>
        public MouseState GetCurrentState()
        {
            // CREATE a new instance of MouseState, called newState. Assigned it to the current mouse state:
            MouseState newState = Mouse.GetState();
            // RETURN newState:
            return newState;
        }
    }
}
