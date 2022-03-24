using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 31-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// ILayer interface
    /// </summary>
    public interface ILayer : IUpdatable
    {
        #region PROPERTIES
        // get-set property for DrawOrder:
        int DrawOrder { get; set; }
        // get property for Entities:
        IList<IEntity> Entities { get; }
        // get-set property for IsActive:
        bool IsActive { get; set; }
        // get-set property for UName:
        string UName { get; set; }
        #endregion

        #region METHODS
        /// <summary>
        /// Draws all entity in the current layer as long as it is active.
        /// </summary>
        /// <param name="pSpriteBatch">A reference to the SpriteBatch to draw the layer entities onto.</param>
        void Draw(SpriteBatch pSpriteBatch);
        #endregion
    }
}
