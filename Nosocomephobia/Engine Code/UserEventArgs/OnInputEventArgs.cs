using Microsoft.Xna.Framework.Input;
using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
namespace Nosocomephobia.Engine_Code.UserEventArgs
{
    public class OnInputEventArgs : EventArgs
    {
        #region FIELDS
        // DECLARE an instance of Keys called _keyInput to store the key that was just pressed by a user:
        private Keys _keyInput;
        #endregion

        #region PROPERTIES
        public Keys KeyInput // read-only property
        {
            get { return _keyInput; } // get method
        }

        #endregion

        /// <summary>
        /// Constructor for objects of class OnInputEventArgs.
        /// </summary>
        /// <param name="keyInput">The key that was just pressed.</param>
        public OnInputEventArgs(Keys keyInput)
        {
            // SET the incoming parameter to the _keyInput:
            _keyInput = keyInput;
        }
    }
}
