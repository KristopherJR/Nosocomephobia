using Nosocomephobia.Engine_Code.UserEventArgs;
using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface IInputPublisher
    {
        /// <summary>
        /// Allows a listener to Subscribe to the InputManager to watch for changes in input. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="subscriber">A reference to the object subscribing to events from InputManager.</param>
        /// <param name="inputHandler">The input handler to Subscribe to the event.</param>
        /// <param name="releaseHandler">The key release handler to Subscribe to the event.</param>
        /// <param name="mouseInputHandler">The mouse input handler to Subscribe to the event.</param>
        void Subscribe(IInputListener subscriber, EventHandler<OnInputEventArgs> inputHandler, EventHandler<OnKeyReleasedEventArgs> releaseHandler, EventHandler<OnMouseInputEventArgs> mouseInputHandler);

        /// <summary>
        /// Allows a listener to unsubscribe from the InputManagers events.
        /// </summary>
        /// <param name="subscriber">A reference to the unsubscribing object of InputManager events.</param>
        /// <param name="inputHandler">The input handler to unsubscribe from the event.</param>
        /// <param name="releaseHandler">The key release handler to unsubscribe from the event.</param>
        /// <param name="mouseInputHandler">The mouse input handler to unsubscribe from the event."</param>
        void Unsubscribe(IInputListener subscriber, EventHandler<OnInputEventArgs> inputHandler, EventHandler<OnKeyReleasedEventArgs> releaseHandler, EventHandler<OnMouseInputEventArgs> mouseInputHandler);
    }
}
