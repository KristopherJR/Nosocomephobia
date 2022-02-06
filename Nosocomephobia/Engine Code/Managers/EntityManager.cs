using Microsoft.Xna.Framework;
using Nosocomephobia.Engine_Code.Exceptions;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.Interfaces.CommandScheduler;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.4, 06-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Managers
{
    class EntityManager : IEntityManager
    {
        #region FIELDS
        // DECLARE a 'List' to contain all objects of type 'IEntity', this 'List' contains a reference to ALL IEntities in the program:
        private List<IEntity> _entityPool;
        // DECLARE an IEntityFactory, call it _entityFactory:
        private IEntityFactory _entityFactory;
        // DECLARE a reference to an ICommandScheduler, call it _commandScheduler. This is a reference to the EngineManagers CommandScheduler Service:
        private ICommandScheduler _commandScheduler;
        #endregion

        #region PROPERTIES
        #endregion

        /// <summary>
        /// Constructor for objects of class EntityManager.
        /// </summary>
        public EntityManager()
        {
            // INITIALIZE the fields:
            _entityPool = new List<IEntity>();
        }

        #region IMPLEMENTATION OF IEntityManager
        /// <summary>
        /// Injects an IEntityFactory to be used by the EntityManager when creating Entities.
        /// </summary>
        /// <param name="pEntityFactory">An ISceneGraphFactory object.</param>
        public void InjectEntityFactory(IEntityFactory pEntityFactory)
        {
            // SET _entityFactory to pEntityFactory:
            _entityFactory = pEntityFactory;
        }

        /// <summary>
        /// Injects a reference to the EngineManagers CommandScheduler Service to be used by the EntityManager when creating Entities.
        /// </summary>
        /// <param name="pCommandScheduler">The EngineManagers CommandScheduler Service.</param>
        public void InjectCommandScheduler(ICommandScheduler pCommandScheduler)
        {
            // SET _commandScheduler to pCommandScheduler:
            _commandScheduler = pCommandScheduler;
        }

        /// <summary>
        /// Creates a new IEntity using the EntityFactory. Adds the newly returned IEntity to the _entityPool.
        /// Automatically assigns a Unique name to the Entity.
        /// </summary>
        /// <typeparam name="T">An object of Type IEntity to be created.</typeparam>
        /// <returns>The newly created IEntity.</returns>
        public IEntity CreateEntity<T>() where T : IEntity, new()
        {
            // USE the EntityFactory to create a new IEntity of the specified type:
            IEntity newEntity = _entityFactory.Create<T>();
            // MAKE the Action<ICommand> property in the Entity point to the ExecuteCommand method in EngineManagers CommandScheduler Service:
            (newEntity as ICommandSender).ScheduleCommand = _commandScheduler.ExecuteCommand;
            // ADD the new Entity to the EntityPool:
            _entityPool.Add(newEntity);
            // RETURN the new IEntity:
            return newEntity;
        }

        /// <summary>
        /// OVERLOAD: Creates a new IEntity using the EntityFactory. Adds the newly returned IEntity to the _entityPool.
        /// Allows the User to specify a name rather than have a randomly generated one.
        /// </summary>
        /// <typeparam name="T">An object of Type IEntity to be created.</typeparam>
        /// <param name="pUName">A Unique name for the new IEntity.</param>
        /// <returns>The newly created IEntity.</returns>
        public IEntity CreateEntity<T>(string pUName) where T : IEntity, new()
        {
            // CHECK that the name provided does not already exist in the _entityPool:
            for(int i = 0; i < _entityPool.Count; i++)
            {
                // IF the name is already in the Entity Pool:
                if (_entityPool[i].UName == pUName)
                {
                    // THROW a new NameNotUniqueException:
                    throw new NameNotUniqueException("The specified name: " + pUName + " is not unique and is already present in the Entity Pool.");
                }
            }

            // USE the EntityFactory to create a new IEntity of the specified type:
            IEntity newEntity = _entityFactory.Create<T>();
            // SET the name of the newEntity to the provided name:
            newEntity.UName = pUName;
            // ADD the new Entity to the EntityPool:
            _entityPool.Add(newEntity);
            // RETURN the new IEntity:
            return newEntity;
        }

        /// <summary>
        /// Destroy an entity by removing it from the entity list.
        /// </summary>
        /// <param name="pUName">The Unique Name of the entity to be destroyed.</param>
        /// <param name="pUID">The Unique ID of the entity to be destroyed.</param>
        public void DestroyEntity(string pUName, int pUID)
        {
            // DECLARE a temporary int to store the index of the object to destroy:
            int temp = 0;
            // ITERATE through the 'entityPool':
            for (int i = 0; i < _entityPool.Count; i++)
            {
                // CHECK if the entity UName matches the provided String or if the entity UID matches the provided int:
                if (_entityPool[i].UName == pUName || (_entityPool[i].UID == pUID))
                {
                    // STORE the index of the item to remove in a temporary int:
                    temp = i;
                }
            }
            // REMOVE the entity from the 'sceneGraph':
            _entityPool.RemoveAt(temp);
        }
        #endregion

        /// <summary>
        /// Defaults update loop for EntityManager.
        /// </summary>
        /// <param name="pGameTime">A reference to GameTime.</param>
        public void Update(GameTime pGameTime)
        {

        }
    }
}
