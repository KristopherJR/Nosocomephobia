using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Components;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Exceptions;
using Nosocomephobia.Engine_Code.Factories;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.3, 31-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Managers
{
    class SceneManager : ISceneManager, IUpdatable
    {
        #region FIELDS
        // DECLARE a new 'IDictionary' storing 'ISceneGraph' objects. Use a string as the key for the SceneGraph names. call it '_sceneGraphs':
        private IDictionary<string, ISceneGraph> _sceneGraphs;
        // DECLARE an ISceneGraphFactory, call it _sceneGraphFactory:
        private ISceneGraphFactory _sceneGraphFactory;
        // DECLARE an IInputManager, call it _inputManager. Stores a reference to the Engines primary InputManager.
        private IInputManager _inputManager;
        // DECLARE an ICollisionManager, call it _collisionManager. Stores a reference to the Engines primary CollisionManager.
        private ICollisionManager _collisionManager;
        // DECLARE a string, call it _previouslyActiveSceneGraph:
        private string _previouslyActiveSceneGraph;
        #endregion

        #region PROPERTIES
        public IDictionary<string, ISceneGraph> SceneGraphs // read-only property
        {
            get { return _sceneGraphs; } // get method
        }
        #endregion

        /// <summary>
        /// Constructor for objects of class SceneManager.
        /// </summary>
        public SceneManager()
        {
            // INITIALISE _sceneGraphs:
            _sceneGraphs = new Dictionary<string, ISceneGraph>();
        }

        #region IMPLEMENTATION OF ISceneManager
        /// <summary>
        /// Injects an ISceneGraphFactory to be used by the SceneManager when creating SceneGraphs.
        /// </summary>
        /// <param name="pSGFactory">An ISceneGraphFactory object.</param>
        public void InjectSceneGraphFactory(ISceneGraphFactory pSGFactory)
        {
            // SET _sceneGraphFactory to pSGFactory:
            _sceneGraphFactory = pSGFactory;
        }

        /// <summary>
        /// Injects a reference to the Engines InputManager so the SceneManager can subscribe entities to Input events when their SceneGraph becomes active.
        /// </summary>
        /// <param name="pInputManager">A reference to the Engines InputManager.</param>
        public void InjectInputManager(IInputManager pInputManager)
        {
            // ASSIGN pInputManager to _inputManager:
            _inputManager = pInputManager;
        }

        /// <summary>
        /// Injects a reference to the Engines CollisionManager so the SceneManager can subscribe entities to collision events when their SceneGraph becomes active.
        /// </summary>
        /// <param name="pInputManager">A reference to the Engines CollisionManager.</param>
        public void InjectCollisionManager(ICollisionManager pCollisionManager)
        {
            // ASSIGN pCollisionManager to _collisionManager:
            _collisionManager = pCollisionManager;
        }

        /// <summary>
        /// Creates a new SceneGraph and adds it to the SceneManagers SceneGraph Dictionary.
        /// </summary>
        /// <param name="pSceneGraphName">A unique name for the new SceneGraph.</param>
        public void CreateSceneGraph(string pSceneGraphName)
        {
            // CHECK if pName is a key already present in the _sceneGraphs Dictionary:
            if(_sceneGraphs.ContainsKey(pSceneGraphName))
            {
                // THROW a NameNotUniqueException:
                throw new NameNotUniqueException("The specified name: " + pSceneGraphName + " is not unique.");
            }
            else
            {
                // CREATE the new SceneGraph using the SceneGraphFactory and store it using the name as its key:
                _sceneGraphs.Add(pSceneGraphName, _sceneGraphFactory.Create<SceneGraph>(pSceneGraphName));
            }
        }

        /// <summary>
        /// OVERLOAD: Creates a new SceneGraph and adds it to the SceneManagers SceneGraph Dictionary.
        /// </summary>
        /// <param name="pSceneGraphName">A unique name for the new SceneGraph.</param>
        /// <param name="pIsActive">Determines whether this SceneGraph is currently 'Active' or not.</param>
        public void CreateSceneGraph(string pSceneGraphName, bool pIsActive)
        {
            // CHECK if pName is a key already present in the _sceneGraphs Dictionary:
            if (_sceneGraphs.ContainsKey(pSceneGraphName))
            {
                // THROW a NameNotUniqueException:
                throw new NameNotUniqueException("The specified name: " + pSceneGraphName + " is not unique.");
            }
            else
            {
                // CREATE the new SceneGraph using the SceneGraphFactory and store it using the name as its key. Pass in the graphs active status:
                _sceneGraphs.Add(pSceneGraphName, _sceneGraphFactory.Create<SceneGraph>(pSceneGraphName, pIsActive));
            }
        }

        /// <summary>
        /// Draws the SceneGraph with the matching name.
        /// </summary>
        /// <param name="pSceneGraphName">The name of the SceneGraph to be drawn.</param>
        /// <param name="pSpriteBatch">A reference to the SpriteBatch that the graph should be drawn onto.</param>
        public void DrawSceneGraph(string pSceneGraphName, SpriteBatch pSpriteBatch)
        {
            // CHECK if pName is a key already present in the _sceneGraphs Dictionary:
            if (_sceneGraphs.ContainsKey(pSceneGraphName))
            {
                // CHECK that the specified SceneGraph is currently 'Active':
                if (_sceneGraphs[pSceneGraphName].IsActive == true)
                {
                    // ITERATE through the specified SceneGraph:
                    foreach (var entity in _sceneGraphs[pSceneGraphName].Entities)
                    {
                        // DRAW each entity to the provided SpriteBatch:
                        (entity as GameEntity).Draw(pSpriteBatch);
                    }
                }
                else
                {
                    // THROW a SceneGraphNotActiveException
                    throw new SceneGraphNotActiveException("The specified SceneGraph: " + pSceneGraphName + " is not active and therefore can not be drawn.");
                }
                
            }
            else
            {
                // THROW an ElementNotFoundException:
                throw new ElementNotFoundException("The specified key: " + pSceneGraphName + " is not present in the SceneGraph Dictionary.");
            }
        }

        /// <summary>
        /// Add an object of type 'IEntity' to the specified 'SceneGraph'.
        /// </summary>
        /// <param name="pSceneGraphName">The unique name of the SceneGraph to add the Entity to.</param>
        /// <param name="pEntity">An object of type IEntity to be added to the Scene Graph.</param>
        public void Spawn(string pSceneGraphName, IEntity pEntity)
        {
            // CHECK if pName is a key already present in the _sceneGraphs Dictionary:
            if (_sceneGraphs.ContainsKey(pSceneGraphName))
            {
                // SPAWN the provided IEntity onto the specified SceneGraph:
                _sceneGraphs[pSceneGraphName].Spawn(pEntity);
            }
            else
            {
                // THROW a ElementNotFoundException:
                throw new ElementNotFoundException("The specified SceneGraph with name: " + pSceneGraphName + " could not be found in the SceneGraph Dictionary.");
            }
        }

        /// <summary>
        /// Removes an Entity from the specified Scene Graph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="pSceneGraphName">The name of the SceneGraph the Entity is in.</param>
        /// <param name="pUName">The unique name of the Entity to despawn.</param>
        /// <param name="pUID">The unique ID of the Entity to despawn.</param>
        public void Despawn(string pSceneGraphName, string pUName, int pUID)
        {
            // CHECK if pName is a key already present in the _sceneGraphs Dictionary:
            if (_sceneGraphs.ContainsKey(pSceneGraphName))
            {
                // DESPAWN the specified IEntity from the SceneGraph:
                _sceneGraphs[pSceneGraphName].Despawn(pUName, pUID);
            }
            else
            {
                // THROW a ElementNotFoundException:
                throw new ElementNotFoundException("The specified SceneGraph with name: " + pSceneGraphName + " could not be found in the SceneGraph Dictionary.");
            }
        }

        /// <summary>
        /// Called whenever the 'Active' SceneGraph changes. Subscribes Entities in the new Active SceneGraph to the InputManagers events.
        /// Unsubscribes entities in previously 'Active' SceneGraph from Input events.
        /// This is because different Scene may has different input functionality.
        /// </summary>
        public void UpdateInputEvents()
        {
            // ITERATE through all SceneGraphs:
            foreach(KeyValuePair<string, ISceneGraph> sceneGraph in _sceneGraphs)
            {
                // CHECK which ones are active:
                if(sceneGraph.Value.IsActive)
                {
                    // IF The name of the active SceneGraph is "GameScene":
                    if (sceneGraph.Value.UName == "GameScene")
                    {
                        // ITERATE through the GameSceneGraph entities:
                        foreach (IEntity entity in sceneGraph.Value.Entities)
                        {
                            // CHECK if the current entity is of type 'Player':
                            if (entity is Player)
                            {
                                // SUBSCRIBE the player to listen for input events and key release events:
                                _inputManager.Subscribe((entity as IInputListener),
                                                        (entity as Player).OnNewInput,
                                                        (entity as Player).OnKeyReleased,
                                                        (entity as Player).OnNewMouseInput);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called whenever the 'Active' SceneGraph changes. Subscribes Entities in the new Active SceneGraph to the CollisionManagers events.
        /// Unsubscribes entities in previously 'Active' SceneGraph from collision events.
        /// This is because different Scene may has different collision functionality.
        /// </summary>
        public void UpdateCollisionEvents()
        {
            // ITERATE through all SceneGraphs:
            foreach (KeyValuePair<string, ISceneGraph> sceneGraph in _sceneGraphs)
            {
                // CHECK which ones are active:
                if (sceneGraph.Value.IsActive)
                {
                    // IF The name of the active SceneGraph is "GameScene":
                    if (sceneGraph.Value.UName == "GameScene")
                    {
                        // POPULATE the CollisionManagers collidables List with objects from the GameSceneGraph:
                        _collisionManager.PopulateCollidables(sceneGraph.Value.Entities);
                    }
                }
            }
        }



        /// <summary>
        /// METHOD: Calls the Update method of each entity in the sceneGraph to make them move.
        /// </summary>
        private void MoveEntities(GameTime gameTime)
        {
            // ITERATE through all SceneGraphs:
            foreach(KeyValuePair<string, ISceneGraph> sceneGraph in _sceneGraphs)
            {
                // CHECK that the SceneGraph is active:
                if(sceneGraph.Value.IsActive == true)
                {
                    // THEN move all Entities in that SceneGraph:
                    foreach (IEntity entity in sceneGraph.Value.Entities)
                    {
                        // CALL the entity's Update method and pass in GameTime:
                        entity.Update(gameTime);
                    }     
                }
            } 
        }
        #endregion

        #region IMPLEMENTATION OF IUpdatable
        /// <summary>
        /// Default Update method for objects implementing the ISceneManager interface.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // CALL the MoveEntities() method:
            this.MoveEntities(gameTime);
        }
        #endregion
    }
}
