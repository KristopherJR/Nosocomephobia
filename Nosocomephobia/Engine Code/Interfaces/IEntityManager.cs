using Nosocomephobia.Engine_Code.Interfaces.CommandScheduler;
using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.4, 06-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface IEntityManager : IService, IUpdatable
    {
        #region PROPERTIES
        #endregion
        /// <summary>
        /// Injects an IEntityFactory to be used by the EntityManager when creating Entities.
        /// </summary>
        /// <param name="pEntityFactory">An ISceneGraphFactory object.</param>
        void InjectEntityFactory(IEntityFactory pEntityFactory);

        /// <summary>
        /// Injects a reference to the EngineManagers CommandScheduler Service to be used by the EntityManager when creating Entities.
        /// </summary>
        /// <param name="pCommandScheduler">The EngineManagers CommandScheduler Service.</param>
        void InjectCommandScheduler(ICommandScheduler pCommandScheduler);

        /// <summary>
        /// Creates a new IEntity using the EntityFactory. Adds the newly returned IEntity to the _entityPool.
        /// Automatically assigns a Unique name to the Entity.
        /// </summary>
        /// <typeparam name="T">An object of Type IEntity to be created.</typeparam>
        /// <returns>The newly created IEntity.</returns>
        IEntity CreateEntity<T>() where T : IEntity, new();

        /// <summary>
        /// OVERLOAD: Creates a new IEntity using the EntityFactory. Adds the newly returned IEntity to the _entityPool.
        /// Allows the User to specify a name rather than have a randomly generated one.
        /// </summary>
        /// <typeparam name="T">An object of Type IEntity to be created.</typeparam>
        /// <param name="pUName">A Unique name for the new IEntity.</param>
        /// <returns>The newly created IEntity.</returns>
        IEntity CreateEntity<T>(string pUName) where T : IEntity, new();

        /// <summary>
        /// Destroy an entity by removing it from the entity list.
        /// </summary>
        /// <param name="pUName">The Unique Name of the entity to be destroyed.</param>
        /// <param name="pUID">The Unique ID of the entity to be destroyed.</param>
        void DestroyEntity(string pUName, int pUID);
    }
}
