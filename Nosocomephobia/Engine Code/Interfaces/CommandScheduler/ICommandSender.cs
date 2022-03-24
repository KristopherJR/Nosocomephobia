using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 14-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// ICommandSender Interface. Implemented by Entity, allows an Entity to schedule a Command (Action<ICommand>).
    /// </summary>
    public interface ICommandSender
    {
        /// <summary>
        /// ScheduleCommand property. Returns/Sets an Action of type ICommand. Points to the CommandSchedulers ExecuteCommand method.
        /// </summary>
        Action<ICommand> ScheduleCommand { get; set; }
    }
}
