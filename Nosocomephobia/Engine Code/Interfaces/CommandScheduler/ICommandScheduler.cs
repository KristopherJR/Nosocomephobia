/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 14-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// ICommandScheduler Interface.
    /// </summary>
    public interface ICommandScheduler : IService, IUpdatable
    {
        /// <summary>
        /// ExecuteCommand will trigger a scheduled Entity command when called. The Command is first stored in the ScheduledCommands list to be executed in the Update loop.
        /// </summary>
        /// <param name="pCommand">The command to be executed.</param>
        void ExecuteCommand(ICommand pCommand);
    }
}
