using Microsoft.Xna.Framework;
using Nosocomephobia.Engine_Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 07-02-2022
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
        /// <param name="pCommand">The command to be executed.</param>
        public void ExecuteCommand(ICommand pCommand)
        {
            
        }

        public void Update(GameTime pGameTime)
        {
            // oops
        }
        #endregion
        #endregion METHODS
    }
}
