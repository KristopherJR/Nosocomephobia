using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Interfaces;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
namespace Nosocomephobia.Engine_Code.InputClasses
{
    class KeyboardInput : IKeyboardInput
    {
        #region FIELDS
        #endregion
        #region PROPERTIES
        #endregion
        /// <summary>
        /// Gets the current state of the Keyboard and returns it.
        /// </summary>
        /// <returns>The current state of the keyboard.</returns>
        public KeyboardState GetCurrentState()
        {
            // CREATE a new instance of KeyboardState, called newState. Assigned it to the current Keyboard state:
            KeyboardState newState = Keyboard.GetState();
            // RETURN newState:
            return newState;
        }
    }
}
