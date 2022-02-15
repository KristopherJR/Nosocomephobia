using Nosocomephobia.Engine_Code.UserEventArgs;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 15-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface ICollisionEventListener
    {
        /// <summary>
        /// Called whenever a Collision Event involving the Entity occurs.
        /// </summary>
        /// <param name="source">The object calling the collision event.</param>
        /// <param name="args">event information including the object that was collided with (colidee).</param>
        void OnCollision(object source, OnCollisionEventArgs args);
    }
}
