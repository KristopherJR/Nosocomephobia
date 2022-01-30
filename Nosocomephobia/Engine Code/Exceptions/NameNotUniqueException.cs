using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.2, 30-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Exceptions
{
    /// <summary>
    /// Thrown whenever a user attempts to use a non-unique name.
    /// </summary>
    public class NameNotUniqueException : Exception
    {
        /// <summary>
        /// Constructor for NameNotUniqueException. Passes the error message to base.
        /// </summary>
        /// <param name="pMessage">The message to be passed with the Exception.</param>
        public NameNotUniqueException(string pMessage) : base(pMessage) { }
    }
}
