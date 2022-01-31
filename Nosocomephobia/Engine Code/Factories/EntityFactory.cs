using Nosocomephobia.Engine_Code.Interfaces;
using System.Diagnostics;
/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 31-01-2022
/// Based on code from BlackBoard Generic Factory Pattern by Marc Price.
/// </summary>
namespace Nosocomephobia.Engine_Code.Factories
{
    public class EntityFactory : IEntityFactory
    {
        #region FIELDS
        // DECLARE an int, call it '_idCounter'. Assigns an ID to all entities created with this factory:
        private int _idCounter;
        #endregion
        /// <summary>
        /// Constructor for Class EntityFactory.
        /// </summary>
        public EntityFactory()
        {
            // INITIALISE the _idCounter to 1:
            _idCounter = 1;
        }

        #region METHODS
        public IEntity Create<T>() where T : IEntity, new()
        {
            // CREATE the IEntity as the specified Type, call it newEntity:
            IEntity newEntity = new T();
            // SET a unique id:
            newEntity.UID = _idCounter;
            // SET a unique name:
            newEntity.UName = (newEntity.ToString() + _idCounter);
            // INCREMENT the idCounter:
            _idCounter++;
            // RETURN newEntity:
            return newEntity;
        }
        #endregion
    }
}
