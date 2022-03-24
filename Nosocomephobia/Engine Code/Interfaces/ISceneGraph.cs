using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 31-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// Interface ISceneGraph
    /// </summary>
    public interface ISceneGraph
    {
        #region PROPERTIES
        // get-set property for UName:
        string UName { get; set; }
        // get-set property for IsActive:
        bool IsActive { get; set; }
        // get property for Layers:
        IDictionary<string, ILayer> Layers { get; }
        #endregion

        #region METHODS

        /// <summary>
        /// Creates a new Layer within the current SceneGraph.
        /// </summary>
        /// <param name="pLayerName">Specifies the name of the new Layer.</param>
        /// <param name="pDrawOrder">Specifies the Draw order of the new Layer within the current SceneGraph.</param>
        void CreateLayer(string pLayerName, int pDrawOrder);

        /// <summary>
        /// Draws the current SceneGraph to the provided SpriteBatch.
        /// </summary>
        /// <param name="pSpriteBatch">A reference to the SpriteBatch to draw the SceneGraph to.</param>
        void Draw(SpriteBatch pSpriteBatch);

        /// <summary>
        /// Add an object of type 'IEntity' to the SceneGraph.
        /// </summary>
        /// <param name="pLayerName">The name of the Layer to spawn the entity on.</param>
        /// <param name="pEntity">An object of type IEntity to be added to the Scene Graph.</param>
        void Spawn(string pLayerName, IEntity pEntity);

        /// <summary>
        /// Removes an object from the SceneGraph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="pLayerName">The name of the Layer to despawn the entity from.</param>
        /// <param name="pUName">The Unique Name of the entity to be removed from the Scene Graph.</param>
        /// <param name="pUID">The Unique ID of the entity to be removed from the Scene Graph.</param>
        void Despawn(string pLayerName, string pUName, int pUID);
        #endregion

    }
}
