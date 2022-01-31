using Nosocomephobia.Engine_Code.Factories;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.Managers;
using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 30-01-2022
namespace Nosocomephobia
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // INSTANTIATE the ServiceFactory:
            IServiceFactory serviceFactory = new ServiceFactory();
            // INSTANTIATE the EngineManager:
            IEngineManager engineManager = new EngineManager();
            // INJECT the ServiceFactory into the EngineManager:
            engineManager.InjectServiceFactory(serviceFactory);
            // INSTANTIATE the Kernel and inject the EngineManager:
            using (var game = new Kernel(engineManager))
                game.Run();
        }
    }
}
