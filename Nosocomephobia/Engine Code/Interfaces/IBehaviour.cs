using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 24-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface IBehaviour
    {
        #region PROPERTIES
        // Get-Set property for Entity:
        IEntity Entity { get;  set; }
        #endregion
    }
}
