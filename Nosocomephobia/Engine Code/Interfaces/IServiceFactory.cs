using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 30-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// IServiceFactory Interface
    /// </summary>
    public interface IServiceFactory
    {
        // DECLARE a generic method returning an IService, where the specified type is an IService. Call it Create:
        IService Create<T>() where T : IService, new();
    }
}
