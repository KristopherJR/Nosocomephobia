using Nosocomephobia.Engine_Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 14-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Logic
{
    /// <summary>
    /// Class Command. Takes no parameters for the Action delegate.
    /// </summary>
    public class Command : ICommand
    {
        #region FIELDS
        // DECLARE an Action, call it _action:
        private Action _action;
        #endregion FIELDS
        /// <summary>
        /// Constructor for Command.
        /// </summary>
        /// <param name="pAction">The embedded Action that the Command points to.</param>
        public Command(Action pAction)
        {
            // ASSIGN _action:
            _action = pAction;
        }

        /// <summary>
        /// Executes the Command by calling the method pointed to in _action.
        /// </summary>
        public void Execute()
        {
            // CALL the method that _action points to:
            _action();
        }
    }

    /// <summary>
    /// Class Command<T>. Takes one parameter for the Action delegate.
    /// </summary>
    public class Command<T>  : ICommand
    {
        #region FIELDS
        // DECLARE an Action<T>, call it _action:
        private Action<T> _action;
        // DECLARE an object of type T, call it _parameterOne:
        private T _parameterOne;
        #endregion FIELDS

        #region METHODS
        /// <summary>
        /// Constructor for Command<T>. Assigns local Action.
        /// </summary>
        /// <param name="pAction">The Action to embed in the Command.</param>
        /// /// <param name="pParameterOne">The parameter data for the actions method.</param>
        public Command(Action<T> pAction, T pParameterOne)
        {
            // ASSIGN _parameterOne:
            _parameterOne = pParameterOne;
            // ASSIGN _action:
            _action = pAction;
        }

        /// <summary>
        /// Executes the Command by calling the method pointed to in _action.
        /// </summary>
        public void Execute()
        {
            // INVOKE _action and pass in _parameterOne:
            _action(_parameterOne) ;
        }
        #endregion METHODS
    }

    /// <summary>
    /// Class Command<T1,T2>. Takes two parameters for the Action delegate.
    /// </summary>
    public class Command<T1,T2> : ICommand
    {
        #region FIELDS
        // DECLARE an Action<T1,T2>, call it _action:
        private Action<T1,T2> _action;
        // DECLARE an object of type T1, call it _parameterOne:
        private T1 _parameterOne;
        // DECLARE an object of type T2, call it _parameterTwo:
        private T2 _parameterTwo;
        #endregion FIELDS

        #region METHODS
        /// <summary>
        /// Constructor for Command<T1,T2>. Assigns local Action.
        /// </summary>
        /// <param name="pAction">The Action to embed in the Command.</param>
        /// <param name="pParameterOne">The first parameter data for the actions method.</param>
        /// /// <param name="pParameterTwo">The second parameter data for the actions method.</param>
        public Command(Action<T1,T2> pAction, T1 pParameterOne, T2 pParameterTwo)
        {
            // ASSIGN _parameterOne:
            _parameterOne = pParameterOne;
            // ASSIGN _parameterTwo:
            _parameterTwo = pParameterTwo;
            // assign the parameter to _action:
            _action = pAction;
        }

        /// <summary>
        /// Executes the Command by calling the method pointed to in _action.
        /// </summary>
        public void Execute()
        {
            // INVOKE _action and pass in _parameterOne and _parameterTwo:
            _action(_parameterOne, _parameterTwo);
        }
        #endregion METHODS
    }

    /// <summary>
    /// Class Command<T1,T2,T3>. Takes three parameters for the Action delegate.
    /// </summary>
    public class Command<T1,T2,T3> : ICommand
    {
        #region FIELDS
        // DECLARE an Action<T1,T2,T3>, call it _action:
        private Action<T1,T2,T3> _action;
        // DECLARE an object of type T1, call it _parameterOne:
        private T1 _parameterOne;
        // DECLARE an object of type T2, call it _parameterTwo:
        private T2 _parameterTwo;
        // DECLARE an object of type T3, call it _parameterThree:
        private T3 _parameterThree;
        #endregion FIELDS

        #region METHODS
        /// <summary>
        /// Constructor for Command<T1,T2>. Assigns local Action.
        /// </summary>
        /// <param name="pAction">The Action to embed in the Command.</param>
        /// <param name="pParameterOne">The first parameter data for the actions method.</param>
        /// <param name="pParameterTwo">The second parameter data for the actions method.</param>
        /// <param name="pParameterThree">The third parameter data for the actions method.</param>
        public Command(Action<T1,T2,T3> pAction, T1 pParameterOne, T2 pParameterTwo, T3 pParameterThree)
        {
            // ASSIGN _parameterOne:
            _parameterOne = pParameterOne;
            // ASSIGN _parameterTwo:
            _parameterTwo = pParameterTwo;
            // ASSIGN _parameterThree:
            _parameterThree = pParameterThree;
            // assign the parameter to _action:
            _action = pAction;
        }

        /// <summary>
        /// Executes the Command by calling the method pointed to in _action.
        /// </summary>
        public void Execute()
        {
            // INVOKE _action and pass in _parameterOne, _parameterTwo and _parameterThree:
            _action(_parameterOne, _parameterTwo, _parameterThree);
        }
        #endregion METHODS
    }

    /// <summary>
    /// Class Command<T1,T2,T3,T4>. Takes four parameters for the Action delegate.
    /// </summary>
    public class Command<T1,T2,T3,T4> : ICommand
    {
        #region FIELDS
        // DECLARE an Action<T1,T2,T3,T4>, call it _action:
        private Action<T1,T2,T3,T4> _action;
        // DECLARE an object of type T1, call it _parameterOne:
        private T1 _parameterOne;
        // DECLARE an object of type T2, call it _parameterTwo:
        private T2 _parameterTwo;
        // DECLARE an object of type T3, call it _parameterThree:
        private T3 _parameterThree;
        // DECLARE an object of type T3, call it _parameterThree:
        private T4 _parameterFour;
        #endregion FIELDS

        #region METHODS
        /// <summary>
        /// Constructor for Command<T1,T2>. Assigns local Action.
        /// </summary>
        /// <param name="pAction">The Action to embed in the Command.</param>
        /// <param name="pParameterOne">The first parameter data for the actions method.</param>
        /// <param name="pParameterTwo">The second parameter data for the actions method.</param>
        /// <param name="pParameterThree">The third parameter data for the actions method.</param>
        /// <param name="pParameterFour">The fourth parameter data for the actions method.</param>
        public Command(Action<T1,T2,T3,T4> pAction, T1 pParameterOne, T2 pParameterTwo, T3 pParameterThree, T4 pParameterFour)
        {
            // ASSIGN _parameterOne:
            _parameterOne = pParameterOne;
            // ASSIGN _parameterTwo:
            _parameterTwo = pParameterTwo;
            // ASSIGN _parameterThree:
            _parameterThree = pParameterThree;
            // ASSIGN _parameterFour:
            _parameterFour = pParameterFour;
            // assign the parameter to _action:
            _action = pAction;
        }

        /// <summary>
        /// Executes the Command by calling the method pointed to in _action.
        /// </summary>
        public void Execute()
        {
            // INVOKE _action and pass in _parameterOne, _parameterTwo, _parameterThree and _parameterFour:
            _action(_parameterOne, _parameterTwo, _parameterThree, _parameterFour);
        }
        #endregion METHODS
    }
}
