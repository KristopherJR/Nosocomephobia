using Microsoft.Xna.Framework;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface IUpdatable
    {
        /// <summary>
        /// Default update loop for an IUpdatable.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        void Update(GameTime gameTime);
    }
}
