/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 05-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// ISceneGraphFactory Interface
    /// </summary>
    public interface ISceneGraphFactory : IService
    {
        // DECLARE a generic method returning an ISceneGraph, where the specified type is an ISceneGraph. Call it Create:
        ISceneGraph Create<T>() where T : ISceneGraph, new();

        /// <summary>
        /// OVERLOAD: DECLARE a generic method returning an ISceneGraph, where the specified type is an ISceneGraph. Call it Create:
        /// </summary>
        /// <typeparam name="T">Object of type ISceneGraph.</typeparam>
        /// <param name="pUName">The name of the new SceneGraph.</param>
        /// <returns>The new SceneGraph with the specified name.</returns>
        ISceneGraph Create<T>(string pUName) where T : ISceneGraph, new();

        /// <summary>
        /// OVERLOAD: DECLARE a generic method returning an ISceneGraph, where the specified type is an ISceneGraph. Call it Create:
        /// </summary>
        /// <typeparam name="T">Object of type ISceneGraph.</typeparam>
        /// <param name="pUName">The name of the new SceneGraph.</param>
        /// <param name="pIsActive">A bool determining if the SceneGraph is active or not.</param>
        /// <returns>The new SceneGraph with the specified name and active status.</returns>
        ISceneGraph Create<T>(string pUName, bool pIsActive) where T : ISceneGraph, new();
    }
}
