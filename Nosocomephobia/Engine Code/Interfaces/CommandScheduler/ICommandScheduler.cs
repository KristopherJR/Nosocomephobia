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
    /// ICommandScheduler Interface.
    /// </summary>
    public interface ICommandScheduler : IService
    {
        /// <summary>
        /// ExecuteCommand will trigger a scheduled Entity command when called.
        /// </summary>
        void ExecuteCommand();
    }
}
