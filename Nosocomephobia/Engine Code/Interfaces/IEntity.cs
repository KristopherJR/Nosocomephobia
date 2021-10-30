/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 30/10/21
/// </summary>

namespace Nosocomephobia.Engine_Code.Interfaces
{
    interface IEntity : IUpdatable
    {
        #region PROPERTIES
        int UID { get; set; }
        string UName { get; set; }
        #endregion
    }
}