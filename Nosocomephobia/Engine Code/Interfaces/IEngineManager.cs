using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher Randle
/// Version: 0.3, 17-01-22
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// Interface IEngineManager.
    /// </summary>
    public interface IEngineManager : IUpdatable
    {
        #region PROPERTIES
        // DECLARE a get property for the IDictionary of services:
        IDictionary<Type, IService> Services { get; }
        #endregion

        #region METHODS

        /// <summary>
        /// Injects an IServiceFactory to be used by the EngineManager when creating Engine Services.
        /// </summary>
        /// <param name="pServiceFactory">An IServiceFactory object.</param>
        void InjectServiceFactory(IServiceFactory pServiceFactory);

        /// <summary>
        /// Initialises and adds the services to the _services IDictionary.
        /// </summary>
        void InitialiseServices();

        /// <summary>
        /// A method taking a generic type, where the type is an IService. Returns the requested IService from the _services dictionary.
        /// </summary>
        /// <typeparam name="T">The generic type to be retrieved from the Dictionary.</typeparam>
        /// <returns>The element with the specified type.</returns>
        IService GetService<T>() where T : IService;
        #endregion
    }
}
