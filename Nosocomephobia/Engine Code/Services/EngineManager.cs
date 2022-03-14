using Microsoft.Xna.Framework;
using Nosocomephobia.Engine_Code.Components;
using Nosocomephobia.Engine_Code.Factories;
using Nosocomephobia.Engine_Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher Randle
/// Version: 0.8, 06-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Services
{
    /// <summary>
    /// Class EngineManager. Has an aggregation relationship with all of the Engines services.
    /// </summary>
    public class EngineManager : IEngineManager, IUpdatable
    {
        #region FIELDS
        // DECLARE an IDictionary<Type,IService> used to store all of the Engines Services, call it _services:
        private IDictionary<Type, IService> _services;
        // DECLARE an IServiceFactory, call it _serviceFactory:
        private IServiceFactory _serviceFactory;
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
            // nothing for now
        }

        /// <summary>
        /// Injects an IServiceFactory to be used by the EngineManager when creating Engine Services.
        /// </summary>
        /// <param name="pServiceFactory">An IServiceFactory object.</param>
        public void InjectServiceFactory(IServiceFactory pServiceFactory)
        {
            // SET _serviceFactory to pServiceFactory:
            _serviceFactory = pServiceFactory;
        }

        /// <summary>
        /// Initialises and adds the services to the _services IDictionary.
        /// </summary>
        public void InitialiseServices()
        {
            // INITIALISE _services:
            _services = new Dictionary<Type, IService>();
            // CREATE the service managers using the abstract Service Factory:
            IEntityManager entityManager = (_serviceFactory.Create<EntityManager>() as EntityManager);
            ISceneManager sceneManager = (_serviceFactory.Create<SceneManager>() as SceneManager);
            ICollisionManager collisionManager = (_serviceFactory.Create<CollisionManager>() as CollisionManager);
            IInputManager inputManager = (_serviceFactory.Create<InputManager>() as InputManager);
            INavigationManager navigationManager = (_serviceFactory.Create<NavigationManager>() as NavigationManager);


            // CREATE the CommandScheduler:
            ICommandScheduler commandScheduler = (_serviceFactory.Create<CommandScheduler>() as CommandScheduler);

            // INJECT a SceneGraphFactory into the SceneManager, create it using the abstract Service Factory:
            sceneManager.InjectSceneGraphFactory(_serviceFactory.Create<SceneGraphFactory>() as SceneGraphFactory);
            // INJECT an EntityFactory into the EntityManager, create it using the abstract Service Factory:
            entityManager.InjectEntityFactory(_serviceFactory.Create<EntityFactory>() as EntityFactory);
            // INJECT a reference to the CommandScheduler Service into the EntityManager to be used when setting up Entity Commands on creation:
            entityManager.InjectCommandScheduler(commandScheduler);
            // INJECT the _inputManager and _collisionManager into the _sceneManager for use with handling SceneGraphs:
            sceneManager.InjectInputManager(inputManager);
            sceneManager.InjectCollisionManager(collisionManager);
            
            // ADD the Engine Services to _services:
            _services.Add(typeof(IEntityManager), entityManager);
            _services.Add(typeof(ISceneManager), sceneManager);
            _services.Add(typeof(ICollisionManager), collisionManager);
            _services.Add(typeof(IInputManager), inputManager);
            _services.Add(typeof(INavigationManager), navigationManager);
            _services.Add(typeof(ICommandScheduler), commandScheduler);
        }

        /// <summary>
        /// A method taking a generic type, where the type is an IService. Returns the requested IService from the _services dictionary.
        /// </summary>
        /// <typeparam name="T">The generic type to be retrieved from the Dictionary.</typeparam>
        /// <returns>The element with the specified type.</returns>
        public IService GetService<T>() where T : IService
        {
            if (_services.ContainsKey(typeof(T)))
            {
                return _services[typeof(T)];
            }
            else
            {
                throw new Exception("The requested service does not exist in the EngineManager.");
            }
        }
        /// <summary>
        /// Default Update method for the EngineManager. Updates all of the Engines Services.
        /// </summary>
        /// <param name="pGameTime">a reference to the GameTime.</param>
        public void Update(GameTime pGameTime)
        {
            // ITERATE through the _services Dictionary:
            foreach(KeyValuePair<Type, IService> service in _services)
            {
                // UPDATE each service:
                (service.Value as IUpdatable).Update(pGameTime);
            }
        }
    }
}
