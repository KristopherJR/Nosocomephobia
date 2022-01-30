/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface IEntity : IUpdatable
    {
        #region PROPERTIES
        int UID { get; set; }
        string UName { get; set; }
        #endregion
    }
}