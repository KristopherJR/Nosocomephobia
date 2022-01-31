using Nosocomephobia.Engine_Code.Interfaces;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 30-01-2022
/// Based on code from BlackBoard Generic Factory Pattern by Marc Price.
/// </summary>
namespace Nosocomephobia.Engine_Code.Factories
{
    /// <summary>
    /// Responsible for the creation of the Engines 'services'.
    /// </summary>
    public class ServiceFactory : IServiceFactory
    {
        #region METHODS
        /// <summary>
        /// CREATES and RETURNS a new IService of the specified type, so long as it is an IService.
        /// </summary>
        /// <typeparam name="T">An object of type IService to be created.</typeparam>
        /// <returns>The newly created IService object.</returns>
        public IService Create<T>() where T : IService, new()
        {
            // CREATE the IService as the specified Type, call it newService:
            IService newService = new T();
            // RETURN newService:
            return newService;
        }
        #endregion
    }
}
