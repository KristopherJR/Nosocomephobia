using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.UserEventArgs;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.3, 15-02-2022
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

        #region IMPLEMENTATION OF IBehaviour
        #region PROPERTIES
        // get-set property for Entity:
        public IEntity MyEntity // property
        {
            get { return _myEntity; }
            set { _myEntity = value; }
        }
        #endregion
        #endregion

        #region IMPLEMENTATION OF IUpdateEventListener
        public virtual void OnUpdate(object source, OnUpdateEventArgs args)
        {

        }
        #endregion

    }
}
