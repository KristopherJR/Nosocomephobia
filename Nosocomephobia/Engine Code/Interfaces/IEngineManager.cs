using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher Randle
/// Version: 0.1, 13-12-21
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// Interface IEngineManager.
    /// </summary>
    public interface IEngineManager
    {
        #region PROPERTIES
        // DECLARE a get property for the IList of service managers:
        IList<IServiceManager> ServiceManagers { get; }
        #endregion

        #region METHODS
        /// <summary>
        /// Initialises and adds the service managers to the _serviceManagers list.
        /// </summary>
        void InitialiseServiceManagers();
        #endregion
    }
}
