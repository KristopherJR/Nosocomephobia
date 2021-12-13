using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    interface IEntityManager : IServiceManager
    {
        /// <summary>
        /// Create an entity of the provided generic type (must be an IEntity).
        /// </summary>
        /// <typeparam name="T">The Generic type to be created.</typeparam>
        /// <returns></returns>
        IEntity createEntity<T>() where T : IEntity, new();

        /// <summary>
        /// Destroy an entity by removing it from the entity list.
        /// </summary>
        /// <param name="UName">The Unique Name of the entity to be destroyed.</param>
        /// <param name="UID">The Unique ID of the entity to be destroyed.</param>
        void destroyEntity(String UName, int UID);
    }
}
