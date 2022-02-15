using Microsoft.Xna.Framework;
using Nosocomephobia.Engine_Code.Interfaces;
using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.3, 14-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Entities
{
    public abstract class Entity : IEntity, IEntityInternal, ICommandSender
    {
        #region FIELDS

        // DECLARE an int, call it '_uID':
        private int _uID;
        // DECLARE a string, call it '_uName':
        private string _uName;

        // DECLARE an instance of IBehaviour, call it _updateBehaviourHandler:
        private IBehaviour _behaviour;

        // DECLARE an ICommand, call it _terminateMe:
        private ICommand _terminateMe;
        // DECLARE an ICommand, call it _removeMe:
        private ICommand _removeMe;
        // DECLARE an Action<ICommand>, call it _scheduledCommand:
        protected Action<ICommand> _scheduledCommand;
        #endregion FIELDS

        #region PROPERTIES
        /// <summary>
        /// UID get-set Property.
        /// </summary>
        public int UID
        {
            get { return _uID; }
            set { _uID = value; }
        }
        /// <summary>
        /// UName get-set Property.
        /// </summary>
        public string UName
        {
            get { return _uName; }
            set { _uName = value; }
        }
        /// <summary>
        /// TerminateMe get-set property.
        /// </summary>
        public ICommand TerminateMe
        {
            get { return _terminateMe; }
            set { _terminateMe = value; }   
        }
        /// <summary>
        /// RemoveMe get-set property.
        /// </summary>
        public ICommand RemoveMe
        {
            get { return _removeMe; }
            set { _removeMe = value; }
        }
        /// <summary>
        /// ScheduleCommand property. Returns/Sets an Action of type ICommand. Points to the CommandSchedulers ExecuteCommand method.
        /// </summary>
        public Action<ICommand> ScheduleCommand
        {
            get { return _scheduledCommand; }
            set { _scheduledCommand = value; }
        }
        #endregion PROPERTIES

        #region METHODS
        /// <summary>
        /// Default Update loop of Entity.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        public virtual void Update(GameTime gameTime)
        {
            // do nothing
        }
        #endregion
    }
}