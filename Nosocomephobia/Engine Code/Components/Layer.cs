using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 31-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Components
{
    /// <summary>
    /// Class Layer represents a single layer with-in a SceneGraph.
    /// </summary>
    public class Layer : ILayer
    {
        #region FIELDS
        // DECLARE an int, call it _drawOrder;
        private int _drawOrder;
        // DECLARE an IList<IEntity>, call it _entities:
        private IList<IEntity> _entities;
        // DECLARE a bool, call it _isActive;
        private bool _isActive;
        // DECLARE a string, call it _uName;
        private string _uName;
        #endregion

        #region PROPERTIES
        // get-set property for DrawOrder:
        public int DrawOrder
        {
            get { return _drawOrder; }
            set { _drawOrder = value; }
        }
        // get property for Entities:
        public IList<IEntity> Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }
        // get-set property for IsActive:
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        // get-set property for UName:
        public string UName
        {
            get { return _uName; }
            set { _uName = value; }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor for class Layer.
        /// </summary>
        public Layer()
        {
            // INITIALISE default fields variable values:
            _entities = new List<IEntity>();
            _drawOrder = 1;
            _isActive = true;
        }

        /// <summary>
        /// Draws all entity in the current layer as long as it is active.
        /// </summary>
        /// <param name="pSpriteBatch">A reference to the SpriteBatch to draw the layer entities onto.</param>
        public void Draw(SpriteBatch pSpriteBatch)
        {
            // ITERATE through all entities on the layer:
            for (int i = 0; i < _entities.Count; i++)
            {
                // DRAW each Entity onto the SpriteBatch:
                (_entities[i] as GameEntity).Draw(pSpriteBatch);
            }
        }

        /// <summary>
        /// Default update loop for Layer.
        /// </summary>
        /// <param name="pGameTime"></param>
        public void Update(GameTime pGameTime)
        {
            // CHECK the current layer is active:
            if (_isActive)
            {
                // ITERATE through all entities on the layer:
                foreach (IEntity entity in _entities)
                {
                    // UPDATE each entity:
                    entity.Update(pGameTime);
                }
            }
        }
        #endregion
    }
}
