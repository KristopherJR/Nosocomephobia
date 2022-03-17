using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.4, 31-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface ISceneManager : IService, IUpdatable
    {
        #region PROPERTIES
        /// <summary>
        /// An IDictionary of SceneGraphs.
        /// </summary>
        /// <returns>The SceneGraphs.</returns>
        IDictionary<string, ISceneGraph> SceneGraphs { get; }
        #endregion

        #region METHODS
        /// <summary>
        /// Injects an ISceneGraphFactory to be used by the SceneManager when creating SceneGraphs.
        /// </summary>
        /// <param name="pSGFactory">An IServiceFactory object.</param>
        void InjectSceneGraphFactory(ISceneGraphFactory pSGFactory);

        /// <summary>
        /// Injects a reference to the Engines InputManager so the SceneManager can subscribe entities to Input events when their SceneGraph becomes active.
        /// </summary>
        /// <param name="pInputManager">A reference to the Engines InputManager.</param>
        void InjectInputManager(IInputManager pInputManager);

        /// <summary>
        /// Injects a reference to the Engines CollisionManager so the SceneManager can subscribe entities to collision events when their SceneGraph becomes active.
        /// </summary>
        /// <param name="pInputManager">A reference to the Engines CollisionManager.</param>
        void InjectCollisionManager(ICollisionManager pCollisionManager);

        /// <summary>
        /// Creates a new SceneGraph and adds it to the SceneManagers SceneGraph Dictionary.
        /// </summary>
        /// <param name="pSceneGraphName">A unique name for the new SceneGraph.</param>
        void CreateSceneGraph(string pSceneGraphName);

        /// <summary>
        /// OVERLOAD: Creates a new SceneGraph and adds it to the SceneManagers SceneGraph Dictionary.
        /// </summary>
        /// <param name="pSceneGraphName">A unique name for the new SceneGraph.</param>
        /// <param name="pIsActive">Determines whether this SceneGraph is currently 'Active' or not.</param>
        void CreateSceneGraph(string pSceneGraphName, bool pIsActive);

        /// <summary>
        /// Draws all Active SceneGraphs to the provided SpriteBatch.
        /// </summary>
        /// <param name="pSpriteBatch">A reference to the SpriteBatch that the graphs should be drawn onto.</param>
        void DrawSceneGraphs(SpriteBatch pSpriteBatch);

        /// <summary>
        /// Add an object of type 'IEntity' to the specified 'SceneGraph'.
        /// </summary>
        /// <param name="pSceneGraphName">The unique name of the SceneGraph to add the Entity to.</param>
        /// <param name="pLayerName">The unique name of the Layer within the SceneGraph to add the Entity to.</param>
        /// <param name="pEntity">An object of type IEntity to be added to the Scene Graph.</param>
        void Spawn(string pSceneGraphName, string pLayerName, IEntity pEntity);

        /// <summary>
        /// Removes an Entity from the specified Scene Graph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="pSceneGraphName">The name of the SceneGraph the Entity is in.</param>
        /// <param name="pLayerName">The unique name of the Layer within the SceneGraph to add the Entity to.</param>
        /// <param name="pUName">The unique name of the Entity to despawn.</param>
        /// <param name="pUID">The unique ID of the Entity to despawn.</param>
        void Despawn(string pSceneGraphName, string pLayerName, string pUName, int pUID);

        /// <summary>
        /// Called whenever the 'Active' SceneGraph changes. Subscribes Entities in the new Active SceneGraph to the InputManagers events.
        /// Unsubscribes entities in previously 'Active' SceneGraph from Input events.
        /// This is because different Scene may has different input functionality.
        /// </summary>
        void RefreshInputEvents();

        /// <summary>
        /// Called whenever the 'Active' SceneGraph changes. Subscribes Entities in the new Active SceneGraph to the CollisionManagers events.
        /// Unsubscribes entities in previously 'Active' SceneGraph from collision events.
        /// This is because different Scene may has different collision functionality.
        /// </summary>
        void RefreshCollisionEvents();
        #endregion
    }
}
