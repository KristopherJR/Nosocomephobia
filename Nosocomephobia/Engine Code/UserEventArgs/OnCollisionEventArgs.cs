using Nosocomephobia.Engine_Code.Interfaces;
using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 15-02-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.UserEventArgs
{
    public class OnCollisionEventArgs : EventArgs
    {
        #region FIELDS
        private ICollidable _collidedObject;
        #endregion

        #region PROPERTIES
        public ICollidable CollidedObject
        {
            get { return _collidedObject; }
        }
        #endregion
        /// <summary>
        /// Constructor for OnCollisionEventArgs
        /// </summary>
        public OnCollisionEventArgs(ICollidable pCollidedObject)
        {
            _collidedObject = pCollidedObject;
        }
    }
}
