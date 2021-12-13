using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher Randle
/// Version: 0.2, 13-12-21
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// Interface IEngineManager.
    /// </summary>
    public interface IEngineManager
    {
        #region PROPERTIES
        // DECLARE a get property for the IDictionary of services:
        IDictionary<Type, IService> Services { get; }
        #endregion

        #region METHODS
        /// <summary>
        /// Initialises and adds the services to the _services IDictionary.
        /// </summary>
        void InitialiseServices();

        /// <summary>
        /// A method taking a generic type, where the type is an IService. Returns the requested IService from the _services dictionary.
        /// </summary>
        /// <typeparam name="T">The generic type to be retrieved from the Dictionary.</typeparam>
        /// <returns>The element with the specified type.</returns>
        T GetService<T>(Type pRequestedService) where T : IService;
        #endregion
    }
}
