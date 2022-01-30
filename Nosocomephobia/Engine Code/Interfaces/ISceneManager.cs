using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.2, 13-12-21
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    interface ISceneManager : IService
    {
        #region PROPERTIES
        /// <summary>
        /// A List of SceneGraphs.
        /// </summary>
        /// <returns>The SceneGraphs.</returns>
        IList<ISceneGraph> SceneGraphs { get; }
        #endregion

        #region METHODS
        /// <summary>
        /// Injects an ISceneGraphFactory to be used by the SceneManager when creating SceneGraphs.
        /// </summary>
        /// <param name="pSGFactory">An IServiceFactory object.</param>
        void InjectSceneGraphFactory(ISceneGraphFactory pSGFactory);

        /// <summary>
        /// Creates a new SceneGraph and adds it to the SceneManagers SceneGraph List.
        /// </summary>
        /// <param name="pName">A unique name for the new SceneGraph.</param>
        void CreateSceneGraph(string pName);

        /// <summary>
        /// OVERLOAD: Creates a new SceneGraph and adds it to the SceneManagers SceneGraph List.
        /// </summary>
        /// <param name="pName">A unique name for the new SceneGraph.</param>
        /// <param name="pIsActive">Determines whether this SceneGraph is currently 'Active' or not.</param>
        void CreateSceneGraph(string pName, bool pIsActive);

        /// <summary>
        /// Draws the SceneGraph with the matching name.
        /// </summary>
        /// <param name="pName">The name of the SceneGraph to be drawn.</param>
        /// <param name="pSpriteBatch">A reference to the SpriteBatch that the graph should be drawn onto.</param>
        void DrawSceneGraph(string pName, SpriteBatch pSpriteBatch);

        /// <summary>
        /// Add an object of type 'IEntity' to the specified 'SceneGraph'.
        /// </summary>
        /// <param name="pEntity">An object of type IEntity to be added to the Scene Graph.</param>
        /// <param name="pSceneGraphName">The unique name of the SceneGraph to add the Entity to.</param>
        void Spawn(IEntity pEntity, string pSceneGraphName);

        /// <summary>
        /// Removes an Entity from the specified Scene Graph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="pUName">The unique name of the Entity to despawn.</param>
        /// <param name="pUID">The unique ID of the Entity to despawn.</param>
        /// <param name="pSceneGraphName">The name of the SceneGraph the Entity is in.</param>
        void Despawn(string pUName, int pUID, string pSceneGraphName);

        /// <summary>
        /// Default Update method for objects implementing the ISceneManager interface.
        /// </summary>
        void Update(GameTime gameTime);
        #endregion
    }
}
