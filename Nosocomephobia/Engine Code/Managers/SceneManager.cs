using Microsoft.Xna.Framework;
using Nosocomephobia.Engine_Code.Components;
using Nosocomephobia.Engine_Code.Exceptions;
using Nosocomephobia.Engine_Code.Factories;
using Nosocomephobia.Engine_Code.Interfaces;
using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.2, 30-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Managers
{
    class SceneManager : ISceneManager, IUpdatable
    {
        #region FIELDS
        // DECLARE a new 'IList' storing 'ISceneGraph' objects, call it 'sceneGraphs':
        private IList<ISceneGraph> _sceneGraphs;
        // DECLARE an ISceneGraphFactory, call it _sceneGraphFactory:
        private ISceneGraphFactory _sceneGraphFactory;
        #endregion

        #region PROPERTIES
        public IList<ISceneGraph> SceneGraphs // read-only property
        {
            get { return _sceneGraphs; } // get method
        }
        #endregion

        /// <summary>
        /// Constructor for objects of class SceneManager.
        /// </summary>
        public SceneManager()
        {
            // INITIALIZE fields:
            _sceneGraphs = new List<ISceneGraph>();
        }

        #region IMPLEMENTATION OF ISceneManager
        /// <summary>
        /// Injects an ISceneGraphFactory to be used by the SceneManager when creating SceneGraphs.
        /// </summary>
        /// <param name="pSGFactory">An IServiceFactory object.</param>
        public void InjectSceneGraphFactory(ISceneGraphFactory pSGFactory)
        {
            // SET _sceneGraphFactory to pSGFactory:
            _sceneGraphFactory = pSGFactory;
        }

        /// <summary>
        /// Creates a new SceneGraph and adds it to the SceneManagers SceneGraph List.
        /// </summary>
        /// <param name="pName">A unique name for the new SceneGraph.</param>
        public void CreateSceneGraph(string pName)
        {
            // CHECK if the SceneManager has less than 1 SceneGraph:
            if(_sceneGraphs.Count < 1)
            {
                // CREATE the new SceneGraph and pass in the provided name, add it to _sceneGraphs:
                _sceneGraphs.Add(_sceneGraphFactory.Create<SceneGraph>(pName));
            }
            else
            {
                // ITERATE through the _sceneGraphs:
                for (int i = 0; i < _sceneGraphs.Count; i++)
                {
                    // IF the name of the current SceneGraph matches the provided parameter name:
                    if (_sceneGraphs[i].UName == pName)
                    {
                        // THROW a NameNotUniqueException:
                        throw new NameNotUniqueException("The specified name: " + pName + " is not unique.");
                    }
                    else
                    {
                        // CREATE the new SceneGraph and pass in the provided name, add it to _sceneGraphs:
                        _sceneGraphs.Add(_sceneGraphFactory.Create<SceneGraph>(pName));
                    }
                }
            }  
        }

        /// <summary>
        /// OVERLOAD: Creates a new SceneGraph and adds it to the SceneManagers SceneGraph List.
        /// </summary>
        /// <param name="pName">A unique name for the new SceneGraph.</param>
        /// <param name="pIsActive">Determines whether this SceneGraph is currently 'Active' or not.</param>
        public void CreateSceneGraph(string pName, bool pIsActive)
        {
            // CHECK if the SceneManager has less than 1 SceneGraph:
            if (_sceneGraphs.Count < 1)
            {
                // CREATE the new SceneGraph and pass in the provided name, add it to _sceneGraphs:
                _sceneGraphs.Add(_sceneGraphFactory.Create<SceneGraph>(pName, pIsActive));
            }
            else
            {
                // ITERATE through the _sceneGraphs:
                for (int i = 0; i < _sceneGraphs.Count; i++)
                {
                    // IF the name of the current SceneGraph matches the provided parameter name:
                    if (_sceneGraphs[i].UName == pName)
                    {
                        // THROW a NameNotUniqueException:
                        throw new NameNotUniqueException("The specified name: " + pName + " is not unique.");
                    }
                    else
                    {
                        // CREATE the new SceneGraph and pass in the provided name and active status, add it to _sceneGraphs:
                        _sceneGraphs.Add(_sceneGraphFactory.Create<SceneGraph>(pName, pIsActive));
                    }
                }
            }         
        }

        /// <summary>
        /// Add an object of type 'IEntity' to the specified 'SceneGraph'.
        /// </summary>
        /// <param name="pEntity">An object of type IEntity to be added to the Scene Graph.</param>
        /// <param name="pSceneGraphName">The unique name of the SceneGraph to add the Entity to.</param>
        public void Spawn(IEntity pEntity, string pSceneGraphName)
        {
            // ITERATE through the _sceneGraphs:
            for (int i = 0; i < _sceneGraphs.Count; i++)
            {
                // IF the name of the current SceneGraph matches the provided parameter name:
                if (_sceneGraphs[i].UName == pSceneGraphName)
                {
                    // SPAWN the provided entity onto the SceneGraph:
                    _sceneGraphs[i].Spawn(pEntity);
                }
                else
                {
                    // THROW a ElementNotFoundException:
                    throw new ElementNotFoundException("The specified SceneGraph with name: " + pSceneGraphName + " could not be found in the SceneGraph list.");
                }
            }
        }

        /// <summary>
        /// Removes an Entity from the specified Scene Graph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="pUName">The unique name of the Entity to despawn.</param>
        /// <param name="pUID">The unique ID of the Entity to despawn.</param>
        /// <param name="pSceneGraphName">The name of the SceneGraph the Entity is in.</param>
        public void Despawn(string pUName, int pUID, string pSceneGraphName)
        {
            // ITERATE through the _sceneGraphs:
            for (int i = 0; i < _sceneGraphs.Count; i++)
            {
                // IF the name of the current SceneGraph matches the provided parameter name:
                if (_sceneGraphs[i].UName == pSceneGraphName)
                {
                    // DESPAWN the entity from the SceneGraph:
                    _sceneGraphs[i].Despawn(pUName, pUID);
                }
                else
                {
                    // THROW a ElementNotFoundException:
                    throw new ElementNotFoundException("The specified SceneGraph with name: " + pSceneGraphName + " could not be found in the SceneGraph list.");
                }
            }
        }

        /// <summary>
        /// METHOD: Calls the Update method of each entity in the sceneGraph to make them move.
        /// </summary>
        private void moveEntities(GameTime gameTime)
        {
            // ITERATE through the 'sceneGraphCopy':
            foreach (IEntity entity in _sceneGraphs)
            {
                // CALL the entity's Update method and pass in GameTime:
                entity.Update(gameTime);
            }
        }
        #endregion

        #region IMPLEMENTATION OF IUpdatable
        /// <summary>
        /// Default Update method for objects implementing the ISceneManager interface.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // CALL the moveEntities() method:
            this.moveEntities(gameTime);
        }
        #endregion
    }
}
