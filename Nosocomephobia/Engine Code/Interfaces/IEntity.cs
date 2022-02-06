/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.1, 06-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// Interface IEntity, Implements IUpdatable.
    /// </summary>
    public interface IEntity : IUpdatable
    {
        #region PROPERTIES
        /// <summary>
        /// UID get-set Property.
        /// </summary>
        int UID { get; set; }
        /// <summary>
        /// UName get-set Property.
        /// </summary>
        string UName { get; set; }
        #endregion
    }
}