using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Exceptions;
using Nosocomephobia.Engine_Code.Interfaces;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 31-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Components
{
    /// <summary>
    /// Represents a SceneGraph object to be used and managed by the SceneManager.
    /// </summary>
    public class SceneGraph : ISceneGraph
    {
        #region FIELDS
        // DECLARE an IDictionary<string, ILayer>, call it _layers:
        private IDictionary<string, ILayer> _layers;
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

        // get property for Entities:
        public IDictionary<string, ILayer> Layers
        { 
            get { return _layers; } 
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor for class SceneGraph
        /// </summary>
        public SceneGraph()
        {
            // INITIALISE fields:
            _layers = new Dictionary<string, ILayer>();
            _uName = "NewSceneGraph";
            _isActive = true;
        }

        /// <summary>
        /// Creates a new Layer within the current SceneGraph.
        /// </summary>
        /// <param name="pLayerName">Specifies the name of the new Layer.</param>
        /// <param name="pDrawOrder">Specifies the Draw order of the new Layer within the current SceneGraph.</param>
        public void CreateLayer(string pLayerName, int pDrawOrder)
        {
            // CHECK that the name of the new Layer is unique:
            if(_layers.ContainsKey(pLayerName))
            {
                throw new NameNotUniqueException("The provided Layer name: " + pLayerName + " already exists within this SceneGraph.");
            }
            else
            {
                // CREATE a new ILayer, call it newLayer:
                ILayer newLayer = new Layer();
                // SET the newLayers fields to the provided parameters:
                newLayer.UName = pLayerName;
                newLayer.DrawOrder = pDrawOrder;
                // ADD the newLayer to _layers:
                _layers.Add(pLayerName, newLayer);
            }
        }

        /// <summary>
        /// Draws the current SceneGraph to the provided SpriteBatch.
        /// </summary>
        /// <param name="pSpriteBatch">A reference to the SpriteBatch to draw the SceneGraph to.</param>
        public void Draw(SpriteBatch pSpriteBatch)
        {
            // CHECK the current graph is active:
            if (_isActive)
            {
                // DRAW all entities in each layers, in order of each Layer Draw Order:
                foreach(KeyValuePair<string, ILayer> layer in _layers)
                {
                    // CHECK the layer is active:
                    if(layer.Value.IsActive)
                    {
                        // DRAW the layer:
                        layer.Value.Draw(pSpriteBatch);
                    }
                }
            }
            else
            {
                // THROW a SceneGraphNotActiveException:
                throw new SceneGraphNotActiveException("The current SceneGraph: " + this.UName + "is not active so can not be drawn.");
            }
        }

        /// <summary>
        /// Add an object of type 'IEntity' to the SceneGraph.
        /// </summary>
        /// <param name="pLayerName">The name of the Layer to spawn the entity on.</param>
        /// <param name="pEntity">An object of type IEntity to be added to the Scene Graph.</param>
        public void Spawn(string pLayerName, IEntity pEntity)
        {
            // IF the Layer exists:
            if (_layers.ContainsKey(pLayerName))
            {
                // ADD the provided entity to the specified Layer:
                _layers[pLayerName].Entities.Add(pEntity);
            }
            else
            {
                // THROW an ElementNotFoundException:
                throw new ElementNotFoundException("The specified layer: " + pLayerName + " does not exist in this SceneGraph.");
            }
        }
        /// <summary>
        /// Removes an object from the SceneGraph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="pLayerName">The name of the Layer to despawn the entity from.</param>
        /// <param name="pUName">The Unique Name of the entity to be removed from the Scene Graph.</param>
        /// <param name="pUID">The Unique ID of the entity to be removed from the Scene Graph.</param>
        public void Despawn(string pLayerName, string pUName, int pUID)
        {
            // IF the Layer exists:
            if (_layers.ContainsKey(pLayerName))
            {
                // DECLARE a temporary int to store the index of the object to despawn:
                int temp = 0;
                // ITERATE through the 'sceneGraph':
                for (int i = 0; i < _layers[pLayerName].Entities.Count; i++)
                {
                    // CHECK if the entity UName matches the provided String or if the entity UID matches the provided int:
                    if (_layers[pLayerName].Entities[i].UName == pUName || (_layers[pLayerName].Entities[i].UID == pUID))
                    {
                        // STORE the index of the item to remove in a temporary int:
                        temp = i;
                    }
                }
                // REMOVE the entity from the _entities:
                _layers[pLayerName].Entities.RemoveAt(temp);
            }
            else
            {
                // THROW an ElementNotFoundException:
                throw new ElementNotFoundException("The specified layer: " + pLayerName + " does not exist in this SceneGraph.");
            }   
        }
        #endregion
    }
}
