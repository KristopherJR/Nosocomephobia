using Nosocomephobia.Engine_Code.Interfaces;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
namespace Nosocomephobia.Engine_Code.InputClasses
{
    class Input : IInput
    {
        #region FIELDS
        // DECLARE a reference to IKeyboardInput, call it 'keyboardInput':
        private IKeyboardInput keyboardInput;
        // DECLARE a reference IMouseInput, call it 'mouseInput':
        private IMouseInput mouseInput;
        #endregion

        #region PROPERTIES
        public IKeyboardInput KeyboardInput // read-only property
        {
            get { return keyboardInput; } // get method
        }
        public IMouseInput MouseInput // read-only property
        {
            get { return mouseInput; } // get method
        }
        #endregion

        public Input()
        {
            // INITIALIZE keyboardInput and mouseInput:
            keyboardInput = new KeyboardInput();
            mouseInput = new MouseInput();
        }
    }
}
