/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 30-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface IBehaviour
    {
        #region PROPERTIES
        // Get-Set property for Entity:
        IEntity MyEntity { get; set; }
        #endregion
    }
}
