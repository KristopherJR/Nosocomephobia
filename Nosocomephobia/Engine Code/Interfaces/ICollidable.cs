/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 30/10/21
/// </summary>

namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface ICollidable
    {
        /// <summary>
        /// A Property to determine if the object can be collided with.
        /// </summary>
        bool IsCollidable { get; set; }
    }
}
