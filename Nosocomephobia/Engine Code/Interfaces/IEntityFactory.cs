/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 05-02-2022
/// Based on code from BlackBoard Generic Factory Pattern by Marc Price.
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// IEntityFactory Interface
    /// </summary>
    public interface IEntityFactory : IService
    {
        // DECLARE a generic method returning an IEntity, where the specified type is an IEntity. Call it Create:
        IEntity Create<T>() where T : IEntity, new();
    }
}
