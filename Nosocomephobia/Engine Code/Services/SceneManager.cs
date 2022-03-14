using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Components;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Exceptions;
using Nosocomephobia.Engine_Code.Factories;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.Logic;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.7, 15-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Services
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
        /// <param name="pCollisionManager">A reference to the Engines CollisionManager.</param>
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
        /// Draws all Active SceneGraphs to the provided SpriteBatch.
        /// </summary>
        /// <param name="pSpriteBatch">A reference to the SpriteBatch that the graphs should be drawn onto.</param>
        public void DrawSceneGraphs(SpriteBatch pSpriteBatch)
        {
            // ITERATE through all SceneGraphs:
            foreach(KeyValuePair<string, ISceneGraph> sceneGraph in _sceneGraphs)
            {
                // CHECK that the specified SceneGraph is currently 'Active':
                if (sceneGraph.Value.IsActive)
                {
                    // DRAW each entity to the provided SpriteBatch:
                    sceneGraph.Value.Draw(pSpriteBatch);  
                }
            }
        }

        /// <summary>
        /// Add an object of type 'IEntity' to the specified 'SceneGraph'.
        /// </summary>
        /// <param name="pSceneGraphName">The unique name of the SceneGraph to add the Entity to.</param>
        /// <param name="pLayerName">The unique name of the Layer within the SceneGraph to add the Entity to.</param>
        /// <param name="pEntity">An object of type IEntity to be added to the Scene Graph.</param>
        public void Spawn(string pSceneGraphName, string pLayerName, IEntity pEntity)
        {
            // CHECK if pName is a key already present in the _sceneGraphs Dictionary:
            if (_sceneGraphs.ContainsKey(pSceneGraphName))
            {
                // CREATE a new ICommand, call it removeMe. Make the Command of type <string,string,string,int>. Pass in an action that points to this.Despawn and the newEntities unique name and ID:
                ICommand removeMe = new Command<string,string,string,int>(this.Despawn, pSceneGraphName, pLayerName, pEntity.UName, pEntity.UID);
                // SET the ICommand TerminateMe in the newEntity to terminateMe:
                (pEntity as IEntityInternal).RemoveMe = removeMe;

                // SPAWN the provided IEntity onto the specified SceneGraph:
                _sceneGraphs[pSceneGraphName].Spawn(pLayerName, pEntity);
            }
            else
            {
                // THROW a ElementNotFoundException:
                throw new ElementNotFoundException("The specified SceneGraph with name: " + pSceneGraphName + " could not be found in the SceneGraph Dictionary.");
            }
        }

        // <summary>
        /// Removes an Entity from the specified Scene Graph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="pSceneGraphName">The name of the SceneGraph the Entity is in.</param>
        /// <param name="pLayerName">The unique name of the Layer within the SceneGraph to add the Entity to.</param>
        /// <param name="pUName">The unique name of the Entity to despawn.</param>
        /// <param name="pUID">The unique ID of the Entity to despawn.</param>
        public void Despawn(string pSceneGraphName, string pLayerName, string pUName, int pUID)
        {
            // CHECK if pName is a key already present in the _sceneGraphs Dictionary:
            if (_sceneGraphs.ContainsKey(pSceneGraphName))
            {
                // ITERATE through all Entites on the specified Layer in the specified SceneGraph:
                for (int i = 0; i < _sceneGraphs[pSceneGraphName].Layers[pLayerName].Entities.Count; i++)
                {
                    // IF the current entity matches the provided UID or UName:
                    if (_sceneGraphs[pSceneGraphName].Layers[pLayerName].Entities[i].UName == pUName || _sceneGraphs[pSceneGraphName].Layers[pLayerName].Entities[i].UID == pUID)
                    {
                        
                        // STORE a reference to matching Entity, call it entityToDespawn:
                        IEntity entityToDespawn = _sceneGraphs[pSceneGraphName].Layers[pLayerName].Entities[i];
                        if (_inputManager.Subscribers.Contains((entityToDespawn as IInputListener)))
                        {
                            // REMOVE all references to entityToDespawn from the InputManager, by unsubscribing to its events:
                            _inputManager.Unsubscribe((entityToDespawn as IInputListener),
                                                (entityToDespawn as IInputListener).OnNewInput,
                                                (entityToDespawn as IInputListener).OnKeyReleased,
                                                (entityToDespawn as IInputListener).OnNewMouseInput);
                        
                            // BREAK the for loop:
                            break;
                        }
                    }
                }

                // REMOVE all references of the entity from the Collision Manager:
                _collisionManager.removeCollidable(pUName, pUID);

                // DESPAWN the specified IEntity from the SceneGraph:
                _sceneGraphs[pSceneGraphName].Despawn(pLayerName, pUName, pUID);
                Debug.WriteLine("SCENE MANAGER: Successfully Removed All References to Object: " + pUName);
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
        public void RefreshInputEvents()
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
                        // ITERATE through all layers in the SceneGraph:
                        foreach (KeyValuePair<string, ILayer> layer in sceneGraph.Value.Layers)
                        {
                            // ITERATE through all entities in the layer:
                            foreach(IEntity entity in layer.Value.Entities)
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
        }

        /// <summary>
        /// Called whenever the 'Active' SceneGraph changes. Subscribes Entities in the new Active SceneGraph to the CollisionManagers events.
        /// Unsubscribes entities in previously 'Active' SceneGraph from collision events.
        /// This is because different Scene may has different collision functionality.
        /// </summary>
        public void RefreshCollisionEvents()
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
                        // ITERATE through all layers in the SceneGraph:
                        foreach(KeyValuePair<string, ILayer> layer in sceneGraph.Value.Layers)
                        {
                            _collisionManager.PopulateCollidables(layer.Value.Entities);
                        }  
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
                    // ITERATE through all Layers in that graph:
                    foreach (KeyValuePair<string, ILayer> layer in sceneGraph.Value.Layers)
                    {
                        // Update each Layer:
                        layer.Value.Update(gameTime);
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
