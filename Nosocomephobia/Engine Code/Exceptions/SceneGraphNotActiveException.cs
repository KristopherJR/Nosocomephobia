using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 31-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Exceptions
{
    /// <summary>
    /// Thrown whenever a user attempts to use a non-active SceneGraph.
    /// </summary>
    public class SceneGraphNotActiveException : Exception
    {
        /// <summary>
        /// Constructor for SceneGraphNotActiveException. Passes the error message to base.
        /// </summary>
        /// <param name="pMessage">The message to be passed with the Exception.</param>
        public SceneGraphNotActiveException(string pMessage) : base(pMessage) {}
    }
}
