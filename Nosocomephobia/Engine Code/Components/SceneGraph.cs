using Nosocomephobia.Engine_Code.Interfaces;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 30-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Components
{
    /// <summary>
    /// Represents a SceneGraph object to be used and managed by the SceneManager.
    /// </summary>
    public class SceneGraph : ISceneGraph
    {
        #region FIELDS
        // DECLARE an IList of type IEntity, call it _entities:
        private IList<IEntity> _entities;
        // DECLARE a string, call it _uName:
        private string _uName;
        // DECLARE a bool, call it _isActive:
        private bool _isActive;
        #endregion

        #region PROPERTIES
        // get-set property for UName:
        public string UName
        {
            get { return _uName; }
            set { _uName = value; }
        }
        // get-set property for IsActive:
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor for class SceneGraph
        /// </summary>
        public SceneGraph()
        {
            // INITIALISE fields:
            _entities = new List<IEntity>();
            _uName = "NewSceneGraph";
            _isActive = true;
        }

        /// <summary>
        /// Add an object of type 'IEntity' to the SceneGraph.
        /// </summary>
        /// <param name="pEntity">An object of type IEntity to be added to the Scene Graph.</param>
        public void Spawn(IEntity pEntity)
        {
            // ADD pEntity to _activeEntites:
            _entities.Add(pEntity);
        }
        /// <summary>
        /// Removes an object from the SceneGraph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="pUName">The Unique Name of the entity to be removed from the Scene Graph.</param>
        /// <param name="pUID">The Unique ID of the entity to be removed from the Scene Graph.</param>
        public void Despawn(string pUName, int pUID)
        {
            // DECLARE a temporary int to store the index of the object to despawn:
            int temp = 0;
            // ITERATE through the 'sceneGraph':
            for (int i = 0; i < _entities.Count; i++)
            {
                // CHECK if the entity UName matches the provided String or if the entity UID matches the provided int:
                if (_entities[i].UName == pUName || (_entities[i].UID == pUID))
                {
                    // STORE the index of the item to remove in a temporary int:
                    temp = i;
                }
            }
            // REMOVE the entity from the _entities:
            _entities.RemoveAt(temp);
        }
        #endregion
    }
}
