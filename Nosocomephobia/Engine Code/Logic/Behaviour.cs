using Nosocomephobia.Engine_Code.Interfaces;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 24-01-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Logic
{
    /// <summary>
    /// Behaviour is represent for enacting an Entities behaviour.
    /// </summary>
    public abstract class Behaviour : IBehaviour, IUpdateEventListener
    {
        #region FIELDS
        // DECLARE an IEntity, call it '_myEntity':
        private IEntity _myEntity;

        #endregion

        #region PROPERTIES
        // Set property for Entity:
        public IEntity Entity // property
        {
            set { _myEntity = value; }
        }
        #endregion

        #region IMPLEMENTATION OF IBehaviour

        #endregion

        #region IMPLEMENTATION OF IUpdateEventListener
        public void OnUpdate()
        {
         
        }
        #endregion

    }
}
