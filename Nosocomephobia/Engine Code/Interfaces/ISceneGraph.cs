using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 30-01-2022
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
        #endregion

        #region METHODS
        /// <summary>
        /// Add an object of type 'IEntity' to the SceneGraph.
        /// </summary>
        /// <param name="pEntity">An object of type IEntity to be added to the Scene Graph.</param>
        void Spawn(IEntity pEntity);

        /// <summary>
        /// Removes an object from the SceneGraph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="pUName">The Unique Name of the entity to be removed from the Scene Graph.</param>
        /// <param name="pUID">The Unique ID of the entity to be removed from the Scene Graph.</param>
        void Despawn(String pUName, int pUID);
        #endregion

    }
}
