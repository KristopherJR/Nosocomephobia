using Nosocomephobia.Engine_Code.Interfaces;
/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 30-01-2022
/// Based on code from BlackBoard Generic Factory Pattern by Marc Price.
/// </summary>
namespace Nosocomephobia.Engine_Code.Factories
{
    /// <summary>
    /// Responsible for the creation of the SceneManagers 'Scene Graphs'.
    /// </summary>
    public class SceneGraphFactory : ISceneGraphFactory
    {
        /// <summary>
        /// Creates a new ISceneGraph and returns it.
        /// </summary>
        /// <typeparam name="T">An object of type ISceneGraph.</typeparam>
        /// <returns>A newly created ISceneGraph object.</returns>
        public ISceneGraph Create<T>() where T : ISceneGraph, new()
        {
            // CREATE the ISceneGraph as the specified Type, call it newSceneGraph:
            ISceneGraph newSceneGraph = new T();
            // RETURN newSceneGraph:
            return newSceneGraph;
        }

        /// <summary>
        /// OVERLOAD: DECLARE a generic method returning an ISceneGraph, where the specified type is an ISceneGraph. Call it Create:
        /// </summary>
        /// <typeparam name="T">Object of type ISceneGraph.</typeparam>
        /// <param name="pUName">The name of the new SceneGraph.</param>
        /// <returns>The new SceneGraph with the specified name.</returns>
        public ISceneGraph Create<T>(string pUName) where T : ISceneGraph, new()
        {
            // CREATE the ISceneGraph as the specified Type, call it newSceneGraph:
            ISceneGraph newSceneGraph = new T();
            // SET the name of the SceneGraph to the provided one:
            newSceneGraph.UName = pUName;
            // RETURN newSceneGraph:
            return newSceneGraph;
        }

        /// <summary>
        /// OVERLOAD: DECLARE a generic method returning an ISceneGraph, where the specified type is an ISceneGraph. Call it Create:
        /// </summary>
        /// <typeparam name="T">Object of type ISceneGraph.</typeparam>
        /// <param name="pUName">The name of the new SceneGraph.</param>
        /// <param name="pIsActive">A bool determining if the SceneGraph is active or not.</param>
        /// <returns>The new SceneGraph with the specified name and active status.</returns>
        public ISceneGraph Create<T>(string pUName, bool pIsActive) where T : ISceneGraph, new()
        {
            // CREATE the ISceneGraph as the specified Type, call it newSceneGraph:
            ISceneGraph newSceneGraph = new T();
            // SET the name of the SceneGraph to the provided one:
            newSceneGraph.UName = pUName;
            // SET the active status to the provided one:
            newSceneGraph.IsActive = pIsActive;
            // RETURN newSceneGraph:
            return newSceneGraph;
        }
    }
}
