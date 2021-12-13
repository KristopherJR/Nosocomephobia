using Nosocomephobia.Engine_Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher Randle
/// Version: 0.1, 13-12-21
/// </summary>
namespace Nosocomephobia.Engine_Code.Managers
{
    /// <summary>
    /// Class EngineManager. Has an aggregation relationship with all of the Engines 'service' Managers.
    /// </summary>
    public class EngineManager : IEngineManager
    {
        #region FIELDS
        // DECLARE an IList<IServiceManager> used to store all of the Engines service manager instances, call it _serviceManagers:
        private IList<IServiceManager> _serviceManagers;
        #endregion

        #region PROPERTIES
        // DECLARE a get property for the IList of service managers:
        public IList<IServiceManager> ServiceManagers
        {
            get { return _serviceManagers; }
        }
        #endregion
        /// <summary>
        /// Constructor for class EngineManager.
        /// </summary>
        public EngineManager()
        {
            
        }

        /// <summary>
        /// Initialises and adds the service managers to the _serviceManagers list.
        /// </summary>
        public void InitialiseServiceManagers()
        {
            // INITIALISE _serviceManagers:
            _serviceManagers = new List<IServiceManager>();
            // CREATE the service managers:
            IEntityManager entityManager = new EntityManager();
            ISceneManager sceneManager = new SceneManager();
            ICollisionManager collisionManager = new CollisionManager();
            IInputManager inputManager = new InputManager();
            INavigationManager navigationManager = new NavigationManager();
            // ADD the service managers to _serviceManagers:
            _serviceManagers.Add(entityManager);
            _serviceManagers.Add(sceneManager);
            _serviceManagers.Add(collisionManager);
            _serviceManagers.Add(inputManager);
            _serviceManagers.Add(navigationManager);
        }
    }
}
