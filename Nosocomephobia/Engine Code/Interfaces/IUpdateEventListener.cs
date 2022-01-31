using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 24-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    interface IUpdateEventListener
    {
        /// <summary>
        /// OnUpdate event, fired from Entity and handled by IBehaviour.
        /// </summary>
        void OnUpdate();
    }
}
