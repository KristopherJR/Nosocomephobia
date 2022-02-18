using Microsoft.Xna.Framework;
using Nosocomephobia.Engine_Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.3, 14-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Services
{
    /// <summary>
    /// Class CommandScheduler. Acts as an Engine Service which can schedule commands fired by Entites to be executed at certain times.
    /// </summary>
    public class CommandScheduler : ICommandScheduler, IUpdatable
    {
        #region FIELDS
        // DECLARE a Queue<ICommand>, call it _scheduledCommands:
        private Queue<ICommand> _scheduledCommands;
        #endregion FIELDS

        #region PROPERTIES
        #endregion PROPERTIES

        #region METHODS
        /// <summary>
        /// Constructor for class CommandScheduler.
        /// </summary>
        public CommandScheduler()
        {
            // INITIALISE fields:
            _scheduledCommands = new Queue<ICommand>();
        }

        #region IMPLEMENTATION of ICommandScheduler
        /// <summary>
        /// ExecuteCommand will trigger a scheduled Entity command when called. The Command is first stored in the ScheduledCommands list to be executed in the Update loop.
        /// </summary>
        /// <param name="pCommand">The command to be executed.</param>
        public void ExecuteCommand(ICommand pCommand)
        {
            // QUEUE the provided ICommand by adding it to _scheduledCommands:
            _scheduledCommands.Enqueue(pCommand);
        }
        #endregion IMPLEMENTATION of ICommandScheduler

        #region IMPLEMENTATION of IUpdatable
        /// <summary>
        /// Default update loop for an IUpdatable.
        /// </summary>
        /// <param name="pGameTime">A reference to the GameTime.</param>
        public void Update(GameTime pGameTime)
        {
            // IF there are commands in the Queue:
            if(_scheduledCommands.Count > 0)
            {
                // DEQUEUE the Command and Execute it:
                (_scheduledCommands.Dequeue()).Execute();
            }
        }
        #endregion IMPLEMENTATION of IUpdatable
        #endregion METHODS
    }
}
