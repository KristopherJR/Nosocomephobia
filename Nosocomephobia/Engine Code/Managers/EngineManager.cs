﻿using Nosocomephobia.Engine_Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Author: Kristopher Randle
/// Version: 0.2, 13-12-21
/// </summary>
namespace Nosocomephobia.Engine_Code.Managers
{
    /// <summary>
    /// Class EngineManager. Has an aggregation relationship with all of the Engines services.
    /// </summary>
    public class EngineManager : IEngineManager
    {
        #region FIELDS
        // DECLARE an IDictionary<Type,IService> used to store all of the Engines Services, call it _services:
        private IDictionary<Type, IService> _services;
        #endregion

        #region PROPERTIES
        // DECLARE a get property for the IDictionary of services:
        public IDictionary<Type, IService> Services
        {
            get { return _services; }
        }
        #endregion
        /// <summary>
        /// Constructor for class EngineManager.
        /// </summary>
        public EngineManager()
        {
            
        }

        /// <summary>
        /// Initialises and adds the services to the _services IDictionary.
        /// </summary>
        public void InitialiseServices()
        {
            // INITIALISE _services:
            _services = new Dictionary<Type, IService>();
            // CREATE the service managers:
            IEntityManager entityManager = new EntityManager();
            ISceneManager sceneManager = new SceneManager();
            ICollisionManager collisionManager = new CollisionManager();
            IInputManager inputManager = new InputManager();
            INavigationManager navigationManager = new NavigationManager();
            // ADD the service managers to _services:
            _services.Add(typeof(IEntityManager), entityManager);
            _services.Add(typeof(ISceneManager), sceneManager);
            _services.Add(typeof(ICollisionManager), collisionManager);
            _services.Add(typeof(IInputManager), inputManager);
            _services.Add(typeof(INavigationManager), navigationManager);
        }

        /// <summary>
        /// A method taking a generic type, where the type is an IService. Returns the requested IService from the _services dictionary.
        /// </summary>
        /// <typeparam name="T">The generic type to be retrieved from the Dictionary.</typeparam>
        /// <returns>The element with the specified type.</returns>
        public T GetService<T>(Type pRequestedService) where T : IService
        {
            try
            {
                if (_services.ContainsKey(pRequestedService))
                {
                    IService<Type> returnedService = _services[pRequestedService];
                    return returnedService;

                }
                else
                {
                    throw new Exception("Requested Service does not exist in EngineManager Dictionary.");
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine("An Error occurred..." + e.Message);
            }
        }
    }
}
