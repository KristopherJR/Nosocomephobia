using Microsoft.Xna.Framework;
using Nosocomephobia.Engine_Code.Interfaces;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.0, 01-05-2021
/// </summary>
namespace Nosocomephobia.Engine_Code.Entities
{
    public abstract class Entity : IEntity
    {
        #region FIELDS

        // DECLARE an int, call it 'uID':
        private int uID;

        // DECLARE a string, call it 'uName':
        private string uName;

        #endregion FIELDS

        #region PROPERTIES

        public int UID
        {
            get { return uID; }
            set { uID = value; }
        }

        public string UName
        {
            get { return uName; }
            set { uName = value; }
        }

        #endregion PROPERTIES

        /// <summary>
        /// Default Update loop of Entity.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        public virtual void Update(GameTime gameTime)
        {
            // do nothing
        }
    }
}