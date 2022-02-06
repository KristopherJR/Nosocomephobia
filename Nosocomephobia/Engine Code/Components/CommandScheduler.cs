using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.Interfaces.CommandScheduler;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 06-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Components
{
    /// <summary>
    /// Class CommandScheduler. Acts as an Engine Service which can schedule commands fired by Entites to be executed at certain times.
    /// </summary>
    public class CommandScheduler : ICommandScheduler
    {
        #region FIELDS
        #endregion FIELDS

        #region PROPERTIES
        #endregion PROPERTIES

        #region METHODS
        /// <summary>
        /// Constructor for class CommandScheduler.
        /// </summary>
        public CommandScheduler()
        {

        }

        #region IMPLEMENTATION of ICommandScheduler
        /// <summary>
        /// ExecuteCommand will trigger a scheduled Entity command when called.
        /// </summary>
        public void ExecuteCommand()
        {

        }
        #endregion
        #endregion METHODS
    }
}
