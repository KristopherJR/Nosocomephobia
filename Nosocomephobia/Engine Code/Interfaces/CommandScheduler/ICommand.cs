using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 14-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// Interface ICommand
    /// </summary>
    public interface ICommand
    {
        #region METHODS
        /// <summary>
        /// Executes the Action contained in Command.
        /// </summary>
        void Execute();
        #endregion METHODS
    }

    /// <summary>
    /// Interace ICommand<T>. Used for Commands that take one parameters.
    /// </summary>
    public interface ICommand<T> : ICommand
    {
    }

    /// <summary>
    /// Interace ICommand<T1,T2>. Used for Commands that take two parameters.
    /// </summary>
    public interface ICommand<T1,T2> : ICommand
    {
    }
}
