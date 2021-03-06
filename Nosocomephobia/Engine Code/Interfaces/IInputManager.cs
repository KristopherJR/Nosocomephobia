using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.3, 14-03-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface IInputManager : IInputPublisher, IService, IUpdatable
    {
        // DECLARE a get property for Input Managers subscribers:
        List<IInputListener> Subscribers { get; }

        /// <summary>
        /// Called when a new input occurs. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="keyInput">Key that was just pressed.</param>
        void OnNewInput(Keys keyInput);

        /// <summary>
        /// Called when a key is released.
        /// </summary>
        /// <param name="keyReleased">Key that was just released.</param>
        void OnKeyReleased(Keys keyReleased);

        /// <summary>
        /// Called when a mouse input occurs.
        /// </summary>
        /// <param name="mouseState">A Snapshot of the mouse state.</param>
        /// <param name="scrollValue">An int representing which direction the scroll wheel is moving.</param>
        void OnNewMouseInput(MouseState mouseState, int scrollValue);
    }
}
