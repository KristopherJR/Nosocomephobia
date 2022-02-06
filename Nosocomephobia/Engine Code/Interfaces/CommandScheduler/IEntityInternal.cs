using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 06-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces.CommandScheduler
{
    /// <summary>
    /// IEntityInternal Interface. Definies ICommand properties for the Command Pattern.
    /// </summary>
    public interface IEntityInternal
    {
        /// <summary>
        /// TerminateMe get-set property.
        /// </summary>
        ICommand TerminateMe { get; set; }
        /// <summary>
        /// RemoveMe get-set property.
        /// </summary>
        ICommand RemoveMe { get; set; }
    }
}
